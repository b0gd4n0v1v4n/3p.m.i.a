using System;
using System.Linq;
using Aimp.Domain;
using Aimp.Logic.Interfaces;
using Aimp.Model;
using Aimp.Model.Documents;
using Aimp.Model.PrintedDocument;
using Aimp.Model.ReportOfClient;
using Aimp.ServiceContract.Services;

namespace Aimp.Console.Wcf
{
    public class ReportOfClientService9 : AimpUsersWcfService8, IClientReportsWcfSerive
    {
        public ClientReports GetClientReports()
        {
            EventLog($"Get client reports");
            try
            {
                var service = IoC.Resolve<IReportOfClientService>();

                var result = new ClientReports();
                result.UserLastNameForFilter = CurrentUser.LastName;
                result.ClientStatusesForFilter = service.GetClientStatuses()
                    .Where(x => x.UsedFilter)
                    .Select(x => x.Name);
                result.Banks = service.GetBanks();

                result.Items = service.GetBankReportClients(CurrentUser).ToList()
                    .GroupBy(g => g.ClientReport)
                    .OrderByDescending(x => x.Key.Date)
                    .Select(x => new ClientReportListItem()
                    {
                        Id = x.Key.Id,
                        BankStatusesReportClient = (from b in result.Banks
                            join bs in x.Select(y => new {y.Bank.Id, y.BankStatus.MiddleName})
                                on b.Id equals bs.Id
                                into statusDefault
                            from bs in statusDefault.DefaultIfEmpty()
                            select bs?.MiddleName).ToArray(),
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
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public ClientReport GetNewClientReport()
        {
            EventLog($"Get new client report");
            try
            {
                var service = IoC.Resolve<IReportOfClientService>();

                return new ClientReport()
            {
                Document = new ClientReportDocument(),
                Banks = service.GetBanks(),
                BankStatuses = service.GetBankStatuses(),
                ClientStatuses = service.GetClientStatuses(),
                CreditProgramms = service.GetCreditProgramms()
            };
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public ClientReport GetClientReport(int id)
        {
            EventLog($"Get client report id: {id}");
            try
            {
                var service = IoC.Resolve<IReportOfClientService>();

                var document = service.GetDocument(id);

            var clientStatuses = service.GetClientStatuses();

            var creditProgramms = service.GetCreditProgramms();

            var banks = service.GetBanks();

            var bankStatuses = service.GetBankStatuses();

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
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public void SaveClientReport(ClientReportDocument document)
        {
            EventLog($"Save client report id: {document.Id}");
            try
            {
                IoC.Resolve<IReportOfClientService>().SaveDocument(document);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public void DeleteClientReport(ClientReportDocument document)
        {
            EventLog($"Delete client report id: {document.Id}");
            try
            {
                IoC.Resolve<IReportOfClientService>().DeleteDocument(document);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public ExcelPrintedDocument GetClientReportPrintedDocument(ClientReports reports)
        {
            EventLog($"Get new client report");
            try
            {
                return (ExcelPrintedDocument)IoC.Resolve<IReportOfClientService>().PrintReport(reports);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }
    }
}