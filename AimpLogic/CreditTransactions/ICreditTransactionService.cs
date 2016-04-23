using Models.Documents;
using Models.Entities;
using Models.PrintedDocument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AimpLogic.CreditTransactions
{
    interface ICreditTransactionService : IDisposable
    {
        IQueryable<CreditTransaction> GetCreditTransactions();
        CreditTransactionDocument GetDocument(int id);
        void SaveDocument(CreditTransactionDocument document);
        void DeleteDocument(CreditTransactionDocument document);
        IQueryable<CreditTransaction> GetCashTransactions();
        IQueryable<PrintedDocumentTemplate> GetPrintedDocumentTemplates();
        IQueryable<Creditor> GetCreditors();
        IQueryable<Requisit> GetRequisits();
        WordPrintedDocument GetPrintedDocument(int idTransaction, string name);
    }
}
