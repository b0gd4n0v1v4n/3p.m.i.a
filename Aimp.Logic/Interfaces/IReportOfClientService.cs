using Aimp.Model.Documents;
using Aimp.Model.PrintedDocument;
using Aimp.Model.ReportOfClient;
using Entities;
using System.Collections.Generic;
using System.Linq;

namespace Aimp.Logic.Interfaces
{
    public interface IReportOfClientService
    {
        ClientReportDocument GetDocument(int id);
        void SaveDocument(ClientReportDocument document);
        void DeleteDocument(ClientReportDocument document);
        IEnumerable<BankStatus> GetBankStatuses();
        IEnumerable<Bank> GetBanks();
        IEnumerable<CreditProgramm> GetCreditProgramms();
        IEnumerable<ClientStatus> GetClientStatuses();
        IQueryable<BankReportClient> GetBankReportClients(User user);
        IPrintedDocument PrintReport(ClientReports report);
    }
}
