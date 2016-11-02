using Aimp.Model;
using Aimp.Model.Documents;
using Aimp.Model.PrintedDocument;
using Entities;
using System.Collections.Generic;
using System.Linq;

namespace Aimp.Logic.Interfaces
{
    public interface ICashTransactionService
    {
        void SaveDocument(CashTransactionDocument document,User user);
        CashTransactionDocument GetDocument(int id);
        void DeleteDocument(CashTransactionDocument document);
        IQueryable<CashTransaction> GetCashTransactions(User user);
        IQueryable<PrintedDocumentTemplate> GetPrintedDocumentTemplates();
        WordPrintedDocument GetPrintedDocument(int idTransaction, string name);
        IEnumerable<EntityName> GetPrintedList();
    }
}
