using AimpConsole.Helpers;
using AimpLogic.CashTransactions;
using AimpLogic.Logic;
using Models;
using Models.CashTransact;
using Models.Documents;
using Models.PrintedDocument;
using System;
using System.Linq;

namespace AimpConsole.Helpers
{
    public class CashTransactionsHelper : AimpHelper,IDisposable
    {
        private ICashTransactionService _logic;
        public void Dispose()
        {
            _logic?.Dispose();
        }
        public CashTransactionsHelper()
        {
            _logic = new CashTransactionService(User.Login, User.Password);
        }
        public CashTransactionsDto GetCashTransactions()
        {
            var items = _logic.GetCashTransactions().Select(x => new CashTransactionListItem()
            {
                Id = x.Id,
                BuyerFullName = x.Buyer.LegalPerson != null ? x.Buyer.LegalPerson.Name : x.Buyer.LastName + " " + x.Buyer.FirstName + " " + x.Buyer.MiddleName,
                SellerFullName = x.Seller.LegalPerson != null ? x.Seller.LegalPerson.Name : x.Seller.LastName + " " + x.Seller.FirstName + " " + x.Seller.MiddleName,
                Date = x.Date,
                DocumentBuyerId = x.Buyer.Document.Id,
                DocumentSellerId = x.Seller.Document.Id,
                Number = x.Number.ToString(),
                NumberProxy = x.NumberProxy,
                TrancportFullName = x.Trancport.Model.Name + ", " + x.Trancport.Make.Name,
                PtsId = x.Trancport.CopyPts.Id
            }).OrderByDescending(x=>new {x.Date,x.Number }).ToList(); ;

            return new CashTransactionsDto()
            {
                Items = items
            };
        }
        public CashTransactionDto GetCashTransaction(int id)
        {
            var document = _logic.GetDocument(id);
            var printedDocuments =
                _logic.GetPrintedDocumentTemplates()
                    .ToList()
                    .Select(x=> new KeyValue<string,string>()
                    {
                        Key = x.Type,
                        Value = x.Name
                    });
            return new CashTransactionDto()
            {
                Document = document,
                PrintedDocuments = printedDocuments 
            };
        }
        public void DeleteCashTransaction(CashTransactionDocument document)
        {
            _logic.DeleteDocument(document);
        }
        public void SaveCashTransaction(CashTransactionDocument document)
        {
            _logic.SaveDocument(document);
        }
        public WordPrintedDocumentDto GetPrintedDocument(int idTransaction, string name)
        {
            return  new WordPrintedDocumentDto() { Document = _logic.GetPrintedDocument(idTransaction,name) };
        }
        public PrintedDocumentsListDto GetPrintedList()
        {
            return new PrintedDocumentsListDto()
            {
                List = _logic.GetPrintedList()
            };
        }
    }
}
