using Models.Documents;
using Models.Entities;
using Models.PrintedDocument;
using Models.ReportOfClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AimpLogic.ReportOfClients
{
    public interface IReportOfClientService : IDisposable
    {
        ClientReportDocument GetDocument(int id);
        void SaveDocument(ClientReportDocument document);
        void DeleteDocument(ClientReportDocument document);
        IEnumerable<BankStatus> GetBankStatuses();
        IEnumerable<Bank> GetBanks();
        IEnumerable<CreditProgramm> GetCreditProgramms();
        IEnumerable<ClientStatus> GetClientStatuses();
        IQueryable<BankReportClient> GetBankReportClients();
        IPrintedDocument PrintReport(ClientReports report);
        User GetUser();
    }
}
