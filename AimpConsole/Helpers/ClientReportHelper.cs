using System;
using System.Linq;
using Models.Documents;
using Models.ReportOfClient;
using AimpLogic.Logic;
using AimpLogic.ReportOfClients;
using Models.PrintedDocument;
using Models;

namespace AimpConsole.Helpers
{
    public class ClientReportHelper : AimpHelper,IDisposable
    {
        private IReportOfClientService _logic;
        public void Dispose()
        {
            _logic?.Dispose();
        }

        public ClientReportHelper()
        {
            _logic = new ReportOfClientService(User.Login, User.Password);
        }
        public ClientReports GetClientReports()
        {
            var result = new ClientReports();
            result.UserLastNameForFilter = _logic.GetUser().LastName;
            result.ClientStatusesForFilter = _logic.GetClientStatuses()
                .Where(x => x.UsedFilter)
                .Select(x => x.Name);
            result.Banks = _logic.GetBanks();

            result.Items = _logic.GetBankReportClients().ToList()
                .GroupBy(g => g.ClientReport)
                .OrderByDescending(x => x.Key.Date)
                .Select(x => new ClientReportListItem()
                {
                    Id = x.Key.Id,
                    BankStatusesReportClient = (from b in result.Banks
                                                join bs in x.Select(y => new { y.Bank.Id, y.BankStatus.MiddleName })
                                                on b.Id equals bs.Id
                                                into statusDefault
                                                from bs in statusDefault.DefaultIfEmpty()
                                                select bs?.MiddleName
                                                ).ToArray(),
                    ClientStatusReportClient = x.Key.ClientStatus.Name,
                    DateReportClient = x.Key.Date.ToString(DataFormats.DateFormat),
                    FullNameReportClient = x.Key.FullName,
                    ManagerReportClient = x.Key.User.LastName,
                    PriceTrancportReportClient = x.Key.Price.ToString(),
                    ProgrammCreditReportClient = x.Key.CreditProgramm.Name,
                    SourceInfoReportClient = x.Key.Source,
                    TelefonReportClient = x.Key.Telefon,
                    TotalContributionReportClient = x.Key.TotalContribution?.ToString(),
                    TrancportNameReportClient = x.Key.Trancport
                }).ToList();

            return result;
        }

        public ClientReport GetNewClientReport()
        {
            return new ClientReport()
            {
                Document = new ClientReportDocument(),
                Banks = _logic.GetBanks(),
                BankStatuses = _logic.GetBankStatuses(),
                ClientStatuses = _logic.GetClientStatuses(),
                CreditProgramms = _logic.GetCreditProgramms()
            };
        }

        public ClientReport GetClientReport(int id)
        {
            var document = _logic.GetDocument(id);

            var clientStatuses = _logic.GetClientStatuses();

            var creditProgramms = _logic.GetCreditProgramms();

            var banks = _logic.GetBanks();

            var bankStatuses = _logic.GetBankStatuses();

            var response = new ClientReport()
            {
                Banks = banks,
                BankStatuses = bankStatuses,
                ClientStatuses = clientStatuses,
                CreditProgramms = creditProgramms,
                Document = document
            };

            return response;
        }

        public void SaveDocument(ClientReportDocument document)
        {
            _logic.SaveDocument(document);
        }
        public ExcelPrintedDocument GetPrintedReportList(ClientReports reports)
        {
            return (ExcelPrintedDocument)_logic.PrintReport(reports);
        }
        public void DeleteDocument(ClientReportDocument document)
        {
            _logic.DeleteDocument(document);
        }
    }
}
