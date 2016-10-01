using System;
using Aimp.ServiceContracts.TrancportCards;
using Aimp.Infrastructure.IoC;
using Aimp.DataContext;
using System.Linq;
using Aimp.Entities;
using System.Collections.Generic;
using Aimp.UserRights;

namespace Aimp.Wcf.Services
{
    public class TrancportCardsService : ITrancportCardsContract
    {
        public int AddCardTrancport(int idCommission, DateTime dateStart)
        {
            try
            {

            using (var context = IoC.Resolve<IAimpContext>())
            {
                var oldCard = context.CardsTrancport.All().FirstOrDefault(x => x.CommissionTransactionId == idCommission);
                if (oldCard == null)
                {
                        var newCard = context.CardsTrancport.Create();
                        newCard.CommissionTransactionId = idCommission;
                        newCard.StatusCardTrancport = context.StatusesCardTrancport.All().FirstOrDefault();
                        newCard.DateStart = dateStart;
                    context.CardsTrancport.AddOrUpdate(newCard);
                    context.SaveChanges();
                    return newCard.Id;
                }
                return oldCard.Id;
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public void DeleteCardTrancport(int id)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    context.CardsTrancport.Delete(id);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public CardTrancportsDto GetCardsTrancport()
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    IQueryable<ICardTrancport> queryResult = null;
                    if (CurrentUserProvider.Account.IsAdmin())
                        queryResult = context.CardsTrancport
                            .All(x => x.CommissionTransaction.User, x => x.CommissionTransaction.Trancport.Make, x => x.CommissionTransaction.Trancport.Model, x => x.CommissionTransaction.Owner.LegalPerson);
                    else
                        queryResult = context.CardsTrancport
                            .All(x => x.CommissionTransaction.User, x => x.CommissionTransaction.Trancport.Make, x => x.CommissionTransaction.Trancport.Model, x => x.CommissionTransaction.Owner.LegalPerson)
                            .Where(x => x.CommissionTransaction.User.Id == CurrentUserProvider.Account.Id);

                    var items = queryResult
                    .OrderByDescending(x => new { x.DateStart, x.Number })
                    .Select(x => new CardTrancportListItemDto()
                    {
                        Id = x.Id,
                        DateSale = x.DateSale,
                        DateStart = x.CommissionTransaction.Date,
                        Status = x.StatusCardTrancport.Name,
                        Number = x.Number.ToString(),
                        NumberT = x.NumberT.ToString(),
                        MakeModelTrancport = x.CommissionTransaction.Trancport.Make.Name + ", " + x.CommissionTransaction.Trancport.Model.Name,
                        ColorTrancport = x.CommissionTransaction.Trancport.Color,
                        Price = x.CommissionTransaction.Price.ToString(),
                        YearTrancport = x.CommissionTransaction.Trancport.Year.ToString(),
                        Source = x.CommissionTransaction.SourceTrancport.Name,
                        Manager = x.ManagerSeller,
                        User = x.CommissionTransaction.User.LastName
                    }).ToList();

                    return new CardTrancportsDto()
                    {
                        Items = items,
                        StatusesCardForFilerStart = new string[] { context..StatusesCardTrancport.FirstOrDefault()?.Name }
                    };
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public CardTrancportDocument GetCardTrancport(int id)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    var preChekcs = context.PreChecksCardTrancport
                                .All(x => x.CardTrancport.CommissionTransaction.SourceTrancport)
                                .Where(x => x.CardTrancport.Id == id)
                                .ToList();

                    return new CardTrancportDocument()
                    {
                        CardTrancport = context.CardsTrancport.Get(id, x => x.StatusCardTrancport, x => x.CommissionTransaction.Seller, x => x.CommissionTransaction.SourceTrancport, x => x.CommissionTransaction.Owner, x => x.CommissionTransaction.Trancport.Make, x => x.CommissionTransaction.Trancport.Model),
                        PreChecks = preChekcs
                    };
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public IEnumerable<IStatusCardTrancport> GetStatusesCardTrancport()
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                    return context.StatusesCardTrancport.All().ToList();
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public void SaveCardTrancport(CardTrancportDocument document)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    var firstCardTrancport = document.CardTrancport;
                    if (firstCardTrancport.Id == 0)
                    {

                        firstCardTrancport.CommissionTransaction.UserId = CurrentUserProvider.Account.Id;
                        foreach (var iPreCheks in document.PreChecks)
                        {
                            iPreCheks.CardTrancport = firstCardTrancport;

                            context.PreChecksCardTrancport.AddOrUpdate(iPreCheks);
                        }
                    }
                    else
                    {


                        var oldPreCheks = context.PreChecksCardTrancport
                                                   .All()
                                                   .Where(x => x.CardTrancport.Id == firstCardTrancport.Id)
                                                   .ToList();
                        foreach (var iPreCheck in oldPreCheks)
                        {
                            var preCheck =
                                document.PreChecks.FirstOrDefault(
                                    x => x.Id == iPreCheck.Id);
                            if (preCheck == null)
                                context.PreChecksCardTrancport.Delete(iPreCheck);
                            else
                                context.PreChecksCardTrancport.AddOrUpdate(preCheck);
                        }
                        foreach (var iNewPreCheck in document.PreChecks.Where(x => x.Id == 0))
                        {
                            context.PreChecksCardTrancport.AddOrUpdate(iNewPreCheck);
                        }
                    }
                    context.CardsTrancport.AddOrUpdate(firstCardTrancport);
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
