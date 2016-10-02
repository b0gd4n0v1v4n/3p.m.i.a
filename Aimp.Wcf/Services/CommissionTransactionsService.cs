using System;
using System.Collections.Generic;
using Aimp.Entities;
using Aimp.ServiceContracts.CommissionTransactions;
using Aimp.Infrastructure.IoC;
using Aimp.DataContext;
using System.Linq;
using Aimp.PrintedDocument;
using Aimp.ServiceContracts;
using Aimp.UserRights;

namespace Aimp.Wcf.Services
{
    public class CommissionTransactionsService : ICommissionTransactionsContract
    {
        public void DeleteCommission(ICommissionTransaction document)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    context.CommissionTransactions.Delete(document.Id);
                    var dleteCardIds = context.CardsTrancport
                        .All()
                        .Where(x => x.CommissionTransaction.Id == document.Id)
                        .Select(x => x.Id).ToArray();
                    context.CardsTrancport
                        .DeleteRange(dleteCardIds);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public CommissionDto GetCommission(int id)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    var commission = context
                .CommissionTransactions
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
                x => x.Trancport.Type,
                x => x.SourceTrancport);

                    string type = PrintedDocumentTemplateType.Комиссия.ToString();
                    
                    var printedDocuments =
                        context.PrintedDocumentTemplates.All().Where(x => x.Type == type)
                            .ToList()
                            .Select(x => new KeyValue<string, string>()
                            {
                                Key = x.Type,
                                Value = x.Name
                            });
                    return new CommissionDto()
                    {
                        Document = commission,
                        PrintedDocuments = printedDocuments
                    };
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public IEnumerable<CommissionListItem> GetCommissions()
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    IQueryable<ICommissionTransaction> items = null;
                    if (CurrentUserProvider.Account.IsAdmin())
                        items = context.CommissionTransactions.All();
                    else
                        items = context.CommissionTransactions.All().Where(x => x.UserId == CurrentUserProvider.Account.Id);

                    return items
                        .Select(x => new CommissionListItem()
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

                    }).OrderByDescending(x => new { x.Date, x.Number }).ToList();
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public IEnumerable<ISourceTrancport> GetSourceTrancport()
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    return context.SourcesTrancport.All().ToList();
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public void SaveCommission(ICommissionTransaction document)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    if (document.Id == 0)
                        document.UserId = CurrentUserProvider.Account.Id;
                    context.CommissionTransactions.AddOrUpdate(document);
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
