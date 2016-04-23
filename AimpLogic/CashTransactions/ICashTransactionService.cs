using Models;
using Models.Documents;
using Models.Entities;
using Models.PrintedDocument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AimpLogic.CashTransactions
{
    public interface ICashTransactionService : IDisposable
    {
        void SaveDocument(CashTransactionDocument document);
        CashTransactionDocument GetDocument(int id);
        void DeleteDocument(CashTransactionDocument document);
        IQueryable<CashTransaction> GetCashTransactions();
        IQueryable<PrintedDocumentTemplate> GetPrintedDocumentTemplates();
        WordPrintedDocument GetPrintedDocument(int idTransaction,string name);
        IEnumerable<EntityName> GetPrintedList();

        }
}