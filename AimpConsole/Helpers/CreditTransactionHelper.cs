using Models.CreditTransact;
using Models.Documents;
using System.Linq;
using System;
using AimpLogic.CreditTransactions;
using Models.PrintedDocument;

namespace AimpConsole.Helpers
{
    public class CreditTransactionHelper : AimpHelper,IDisposable
    {
        private CreditTransactionService _logic;
        public CreditTransactionHelper() {

            _logic = new CreditTransactionService(User.Login, User.Password);
        }

        public void Dispose()
        {
            _logic?.Dispose();
        }
        public CreditTransactions GetCreditTransactions()
        {
            var items = _logic.GetCreditTransactions().Select(x => new CreditTransactionListItem()
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
                PtsId = x.Trancport.CopyPts.Id,
                AdId = x.AgentDocument.Id,
                DkpId = x.DkpDocument.Id,
                PhotoBuyerId = x.Buyer.PhotoId,
                PhotoSellerId = x.SellerId
            }).ToList();

            return new CreditTransactions()
            {
                Items = items
            };
        }
        public CreditTransactionDto GetCreditTransaction(int id)
        {
            var document = _logic.GetDocument(id);
            var creditors = _logic.GetCreditors().ToList();
            var requsits = _logic.GetRequisits().ToList();
            return new CreditTransactionDto()
            {
                Document = document
            };
        }
        public CreditTransactionInfoDto GetCreditTransactionInfo()
        {
            var creditors = _logic.GetCreditors().ToList();
            var requsits = _logic.GetRequisits().ToList();
            return new CreditTransactionInfoDto()
            {
                Creditors = creditors,
                Requisits = requsits
            };
        }
        public WordPrintedDocumentDto GetPrintedDocument(int idTransaction, string name)
        {
            return new WordPrintedDocumentDto() { Document = _logic.GetPrintedDocument(idTransaction, name) };
        }
        public PrintedDocumentsListDto GetPrintedList()
        {
            return new PrintedDocumentsListDto()
            {
                List = _logic.GetPrintedList()
            };
        }
        public void DeleteCreditTransaction(CreditTransactionDocument document)
        {
            _logic.DeleteDocument(document);
        }

        public void SaveCreditTransaction(CreditTransactionDocument document)
        {
            _logic.SaveDocument(document);
        }
    }
}
