using System;
using System.Linq;
using Aimp.Domain;
using Aimp.Logic.Interfaces;
using Aimp.Model.Documents;
using Aimp.Model.PrintedDocument;
using Aimp.Model.ReportOfClient;
using Aimp.ServiceContract.Services;
using Entities;
using System.Collections.Generic;

namespace Aimp.Console.Wcf
{
    public class ClientReportsWcfSerive9 : AimpUsersWcfService8, IClientReportsWcfSerive
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

                result.Items = service.GetBankReportClients(CurrentUser);
                    
                return result;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public ClientReportDto GetNewClientReport()
        {
            EventLog($"Get new client report");
            try
            {
                var service = IoC.Resolve<IReportOfClientService>();

                return new ClientReportDto()
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

        public ClientReportDto GetClientReport(int id)
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

            var response = new ClientReportDto()
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

        public int SaveClientReport(ClientReportDocument document)
        {
            EventLog($"Save client report id: {document.Id}");
            try
            {
                document.UserId = CurrentUser.Id;
                IoC.Resolve<IReportOfClientService>().SaveDocument(document);
                return document.Id;
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

        public ExcelPrintedDocument GetClientReportPrintedDocument(IEnumerable<Bank> banks, IEnumerable<ClientReportListItem> reports)
        {
            EventLog($"Get new client report");
            try
            {
                return (ExcelPrintedDocument)IoC.Resolve<IReportOfClientService>().PrintReport(banks,reports);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }
    }
}