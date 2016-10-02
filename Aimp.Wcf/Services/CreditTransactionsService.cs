using System;
using System.Collections.Generic;
using Aimp.Entities;
using Aimp.ServiceContracts.CreditTransactions;
using Aimp.Infrastructure.IoC;
using Aimp.DataContext;
using System.Linq;
using Aimp.UserRights;

namespace Aimp.Wcf.Services
{
    public class CreditTransactionsService : ICreditTransactionsContract
    {
        public void DeleteCreditTransaction(ICreditTransaction document)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    context.CreditTransactions.Delete(document.Id);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public ICreditTransaction GetCreditTransaction(int id)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    var creditTransaction = context
                                .CreditTransactions
                                .Get(id, x => x.Seller.City,
                                        x => x.Seller.Region,
                                        x => x.Buyer.City,
                                        x => x.Buyer.Region,
                                        x => x.Owner.City,
                                        x => x.Owner.Region,
                                        x => x.Trancport.Model,
                                        x => x.Trancport.Make,
                                        x => x.Trancport.Category,
                                        x => x.Trancport.EngineType,
                                        x => x.Trancport.Type,
                                        x => x.Requisit,
                                        x => x.Creditor);
                    var ad = context.UserFiles.Create();
                    ad.Id = creditTransaction.AgentDocumentId.HasValue ? creditTransaction.AgentDocumentId.Value : 0;
                    creditTransaction.AgentDocument = ad;
                    var dkp = context.UserFiles.Create();
                    dkp.Id = creditTransaction.DkpDocumentId.HasValue ? creditTransaction.DkpDocumentId.Value : 0;

                    creditTransaction.DkpDocument = dkp;

                    return creditTransaction;
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public CreditTransactionInfoDto GetCreditTransactionInfo()
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    var creditors = context.Creditors.All().ToList();
                    var requsits = context.Requisits.All().ToList();
                    return new CreditTransactionInfoDto()
                    {
                        Creditors = creditors,
                        Requisits = requsits
                    };
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public IEnumerable<CreditTransactionListItem> GetCreditTransactions()
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    IQueryable<ICreditTransaction> items = null;
                    if (CurrentUserProvider.Account.IsAdmin())
                        items = context.CreditTransactions.All();
                    else
                        items = context.CreditTransactions.All().Where(x => x.UserId == CurrentUserProvider.Account.Id);

                    return items.Select(x => new CreditTransactionListItem()
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
                    }).OrderByDescending(x => new { x.Date, x.Number }).ToList();
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public void SaveCreditTransaction(ICreditTransaction document)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    if (document.Id == 0)
                        document.UserId = CurrentUserProvider.User.Id;
                    else
                    {

                        ICreditTransaction dbTransaction = null;

                        if (document.Id != 0)
                            dbTransaction = context.CreditTransactions.Get(document.Id, x => x.DkpDocument, x => x.AgentDocument);

                        UserFileCheck.AddOrUpdate(context, document, document.DkpDocument, dbTransaction?.DkpDocument);
                        UserFileCheck.AddOrUpdate(context, document, document.AgentDocument, dbTransaction?.AgentDocument);
                    }
                    context.CreditTransactions.AddOrUpdate(document);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }
    }
}
