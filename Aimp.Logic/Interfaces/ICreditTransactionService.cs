using Aimp.Model.Documents;
using Aimp.Model.PrintedDocument;
using Entities;
using System.Linq;

namespace Aimp.Logic.Interfaces
{
    public interface ICreditTransactionService
    {
        IQueryable<CreditTransaction> GetCreditTransactions(User user);
        CreditTransactionDocument GetDocument(int id);
        void SaveDocument(CreditTransactionDocument document);
        void DeleteDocument(CreditTransactionDocument document);
        IQueryable<PrintedDocumentTemplate> GetPrintedDocumentTemplates();
        IQueryable<Creditor> GetCreditors();
        IQueryable<Requisit> GetRequisits();
        WordPrintedDocument GetPrintedDocument(int idTransaction, string name);
    }
}
