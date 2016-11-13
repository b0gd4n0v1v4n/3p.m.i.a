using System.Collections.Generic;
using Aimp.Model;
using Aimp.Model.Documents;
using Aimp.Model.PrintedDocument;
using Entities;
using System.Linq.Expressions;
using System;

namespace Aimp.Logic.Interfaces
{
    public interface ICashTransactionService
    {
        void SaveDocument(CashTransactionDocument document);
        CashTransactionDocument GetDocument(int id);
        void DeleteDocument(CashTransactionDocument document);
        IEnumerable<CashTransaction> GetCashTransactions(User user, params Expression<Func<CashTransaction, object>>[] includes);
        IEnumerable<PrintedDocumentTemplate> GetPrintedDocumentTemplates();
        WordPrintedDocument GetPrintedDocument(int idTransaction, string name);
        IEnumerable<EntityName> GetPrintedList();
    }
}