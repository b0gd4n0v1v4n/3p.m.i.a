using System;
using System.Collections.Generic;
using System.Linq;
using Aimp.Domain;
using Aimp.Logic.Interfaces;
using Aimp.Model;
using Aimp.Model.CreditTransact;
using Aimp.Model.Documents;
using Aimp.ServiceContract.Services;

namespace Aimp.Console.Wcf
{
    public class CreditTransactionWcfService4 : DataContextWcfService3, ICreditTransactionWcfService
    {
        public IEnumerable<CreditTransactionListItem> GetCreditTransactions()
        {
            EventLog($"Get credit transactions");
            try
            {
                return IoC.Resolve<ICreditTransactionService>()
                    .GetCreditTransactions(CurrentUser, x => x.Buyer,
                             x => x.Buyer.LegalPerson,
                             x => x.Seller,
                             x => x.Seller.LegalPerson,
                             x => x.Trancport,
                             x => x.Trancport.Make,
                             x => x.Trancport.Model)
                    .Select(x => new CreditTransactionListItem()
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
                        DateTime = x.Date,
                        DocumentBuyerId = x.Buyer.DocumentId,
                        DocumentSellerId = x.Seller.DocumentId,
                        Number = x.Number.ToString(),
                        NumberProxy = x.NumberProxy,
                        TrancportFullName = x.Trancport.Model.Name + ", " + x.Trancport.Make.Name,
                        PtsId = x.Trancport.CopyPtsId,
                        AdId = x.AgentDocumentId,
                        DkpId = x.DkpDocumentId,
                        PhotoBuyerId = x.Buyer.PhotoId,
                        PhotoSellerId = x.Seller.PhotoId
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public CreditTransactionDocument GetCreditTransaction(int id)
        {
            EventLog($"Get credit transaction id: {id}");
            try
            {
                return IoC.Resolve<ICreditTransactionService>().GetDocument(id);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public KeyValue<int,int> SaveCreditTransaction(CreditTransactionDocument document)
        {
            EventLog($"Save credit transaction document id: {document.Id}");
            try
            {
                document.UserId = CurrentUser.Id;
                IoC.Resolve<ICreditTransactionService>().SaveDocument(document);
                return new KeyValue<int, int>(){Key = document.Id,Value = document.Number};
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public void DeleteCreditTransaction(CreditTransactionDocument document)
        {
            EventLog($"Delete credit transaction document id: {document.Id}");
            try
            {
                IoC.Resolve<ICreditTransactionService>().DeleteDocument(document);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public CreditTransactionInfoDto GetCreditTransactionInfo()
        {
            EventLog($"Get credit transaction info");

            try
            {
                var creditors = IoC.Resolve<ICreditTransactionService>().GetCreditors().ToList();
                var requsits = IoC.Resolve<ICreditTransactionService>().GetRequisits().ToList();
                return new CreditTransactionInfoDto()
                {
                    Creditors = creditors,
                    Requisits = requsits
                };
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }
    }
}
