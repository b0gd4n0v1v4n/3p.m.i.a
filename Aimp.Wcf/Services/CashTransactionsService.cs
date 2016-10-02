using System;
using Aimp.Entities;
using Aimp.ServiceContracts.CashTransactions;
using Aimp.Infrastructure.IoC;
using Aimp.DataContext;
using System.Collections.Generic;
using System.Linq;
using Aimp.UserRights;
using Aimp.ServiceContracts;
using Aimp.PrintedDocument;

namespace Aimp.Wcf.Services
{
    public class CashTransactionsService : ICashTransactionsContract
    {
        public void DeleteCashTransaction(ICashTransaction document)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    context.CashTransactions.Delete(document.Id);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public CashTransactionDto GetCashTransaction(int id)
        {
            using (var context = IoC.Resolve<IAimpContext>())
            {
                var cashTransaction = context
                .CashTransactions
        .Get(id, x => x.Seller.City,
                x => x.Seller.Region,
                x => x.Seller.LegalPerson,
                x => x.Buyer.City,
                x => x.Buyer.Region,
                x => x.Buyer.LegalPerson,
                x => x.Owner.City,
                x => x.Owner.Region,
                x => x.Owner.LegalPerson,
                x => x.Trancport.Model,
                x => x.Trancport.Make,
                x => x.Trancport.Category,
                x => x.Trancport.EngineType,
                x => x.Trancport.Type);

                string type = PrintedDocumentTemplateType.Сделка.ToString();
                var printedDocuments =
                context.PrintedDocumentTemplates.All().Where(x => x.Type == type)
                    .ToList()
                    .Select(x => new KeyValue<string, string>()
                    {
                        Key = x.Type,
                        Value = x.Name
                    });
                return new CashTransactionDto()
                {
                    Document = cashTransaction,
                    PrintedDocuments = printedDocuments
                };
            }
        }

        public IEnumerable<CashTransactionListItem> GetCashTransactions()
        {
            using (var context = IoC.Resolve<IAimpContext>())
            {
                if (CurrentUserProvider.Account.IsAdmin())
                    return context.CashTransactions.All().Select(x => new CashTransactionListItem()
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
                    }).OrderByDescending(x => new { x.Date, x.Number })
                    .ToList();
                else
                    return context.CashTransactions
                        .All()
                        .Where(x => x.UserId == CurrentUserProvider.Account.Id)
                        .Select(x => new CashTransactionListItem()
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
                        }).OrderByDescending(x => new { x.Date, x.Number })
                        .ToList(); ;
            }
        }

        public void SaveCashTransaction(ICashTransaction document)
        {
            using (var context = IoC.Resolve<IAimpContext>())
            {
                if (document.Id == 0)
                    document.UserId = CurrentUserProvider.Account.Id;
                context.CashTransactions.AddOrUpdate(document);
                context.SaveChanges();
            }
        }
    }
}
