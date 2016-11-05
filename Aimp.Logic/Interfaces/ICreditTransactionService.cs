using System.Collections.Generic;
using Aimp.Model;
using Aimp.Model.Documents;
using Aimp.Model.PrintedDocument;
using Entities;
using System.Linq.Expressions;
using System;

namespace Aimp.Logic.Interfaces
{
    public interface ICreditTransactionService
    {
        IEnumerable<CreditTransaction> GetCreditTransactions(User user, params Expression<Func<CreditTransaction, object>>[] includes);
        CreditTransactionDocument GetDocument(int id);
        void SaveDocument(CreditTransactionDocument document);
        void DeleteDocument(CreditTransactionDocument document);
        IEnumerable<PrintedDocumentTemplate> GetPrintedDocumentTemplates();
        IEnumerable<EntityName> GetPrintedList();
        IEnumerable<Creditor> GetCreditors();
        IEnumerable<Requisit> GetRequisits();
        WordPrintedDocument GetPrintedDocument(int idTransaction, string name);
    }
}
