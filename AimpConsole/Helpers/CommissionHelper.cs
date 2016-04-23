using AimpLogic.Commission;
using Models;
using Models.Commission;
using Models.Documents;
using Models.PrintedDocument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AimpConsole.Helpers
{
    public class CommissionHelper : AimpHelper, IDisposable
    {
        private ICommissionService _logic;
        public void Dispose()
        {
            _logic?.Dispose();
        }
        public CommissionHelper()
        {
            _logic = new CommissionService(User.Login, User.Password);
        }
        public CommissionsDto GetCommissions()
        {
            var items = _logic.GetCommissions().Select(x => new CommissionListItem()
            {
                Id = x.Id,
                SellerFullName = x.Seller.LegalPerson != null ? x.Seller.LegalPerson.Name : x.Seller.LastName + " " + x.Seller.FirstName + " " + x.Seller.MiddleName,
                Date = x.Date,
                DocumentSellerId = x.Seller.Document.Id,
                Number = x.Number.ToString(),
                NumberProxy = x.NumberProxy,
                TrancportFullName = x.Trancport.Model.Name + ", " + x.Trancport.Make.Name,
                PtsId = x.Trancport.CopyPts.Id,
                Parking = x.Parking.ToString(),
                Commission = x.Commission.ToString()

            }).ToList(); ;

            return new CommissionsDto()
            {
                Items = items
            };
        }
        public SourcesTrancportDto GetSourcesTrancport()
        {
            var items = _logic.GetSourcesTrancport().ToList();

            return new SourcesTrancportDto()
            {
                Items = items
            };
        }
        public CommissionDto GetCommission(int id)
        {
            var document = _logic.GetDocument(id);
            var printedDocuments =
                _logic.GetPrintedDocumentTemplates()
                    .ToList()
                    .Select(x => new KeyValue<string, string>()
                    {
                        Key = x.Type,
                        Value = x.Name
                    });
            return new CommissionDto()
            {
                Document = document,
                PrintedDocuments = printedDocuments
            };
        }
        public void DeleteCommission(CommissionDocument document)
        {
            _logic.DeleteDocument(document);
        }
        public void SaveCommision(CommissionDocument document)
        {
            _logic.SaveDocument(document);
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
    }
}