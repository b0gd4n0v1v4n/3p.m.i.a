using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aimp.Domain;
using Aimp.Logic.Interfaces;
using Aimp.Model;
using Aimp.Model.CashTransact;
using Aimp.Model.Documents;
using Aimp.ServiceContract.Services;

namespace Aimp.Console.Wcf
{
    public class CashTransactionWcfService6 :CommisionWcfService5, ICashTransactionWcfService
    {
        public IEnumerable<CashTransactionListItem> GetCashTransactions()
        {
            EventLog($"Get cash transactions");
            try
            {
                return IoC.Resolve<ICashTransactionService>()
                    .GetCashTransactions(CurrentUser)
                    .Select(x => new CashTransactionListItem()
                    {
                        Id = x.Id,
                        BuyerFullName =
                            x.Buyer.LegalPerson != null
                                ? x.Buyer.LegalPerson.Name
                                : x.Buyer.LastName + " " + x.Buyer.FirstName + " " + x.Buyer.MiddleName,
                        SellerFullName =
                            x.Seller.LegalPerson != null
                                ? x.Seller.LegalPerson.Name
                                : x.Seller.LastName + " " + x.Seller.FirstName + " " + x.Seller.MiddleName,
                        Date = x.Date,
                        DocumentBuyerId = x.Buyer.Document.Id,
                        DocumentSellerId = x.Seller.Document.Id,
                        Number = x.Number.ToString(),
                        NumberProxy = x.NumberProxy,
                        TrancportFullName = x.Trancport.Model.Name + ", " + x.Trancport.Make.Name,
                        PtsId = x.Trancport.CopyPts.Id
                    }).OrderByDescending(x => new {x.Date, x.Number}).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public CashTransactionDto GetCashTransaction(int id)
        {
            EventLog($"Get cash transaction id: {id}");
            try
            {
                var service = IoC.Resolve<ICashTransactionService>();

                var document = service.GetDocument(id);
                var printedDocuments =
                    service.GetPrintedDocumentTemplates()
                        .ToList()
                        .Select(x => new KeyValue<string, string>()
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
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public void SaveCashTransaction(CashTransactionDocument document)
        {
            EventLog($"Save cash transaction id: {document.Id}");
            try
            {
                IoC.Resolve<ICashTransactionService>().SaveDocument(document);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public void DeleteCashTransaction(CashTransactionDocument document)
        {
            EventLog($"Delete cash transaction id: {document.Id}");
            try
            {
                IoC.Resolve<ICashTransactionService>().DeleteDocument(document);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }
    }
}
