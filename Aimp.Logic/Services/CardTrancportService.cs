using Aimp.DataAccess.Interfaces;
using Aimp.Domain;
using Aimp.Logic.Extensions;
using Aimp.Logic.Interfaces;
using Aimp.Logic.Sequnces;
using Aimp.Model.Documents;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Aimp.Logic.Services
{
    public class CardTrancportService: ICardTrancportService
    {
        //private readonly IYearNumberSequence<int> _sequnce;
        //private readonly object _sync = new object();

        //public CardTrancportService()
        //{
        //    using (var context = IoC.Resolve<IDataContext>())
        //    {
        //        var beginSequnce = context.CardsTrancport
        //            .All()
        //            .GroupBy(x => x.CommissionTransaction.Date.Year)
        //            .Select(x => new { x.Key, x.OrderByDescending(m => m.Number).FirstOrDefault().CommissionTransaction.Number })
        //            .ToDictionary(x => x.Key, x => x.Number + 1);

        //        _sequnce = new YearNumberSequence(beginSequnce);
        //    }
        //}
        public int AddCardTrancport(int idCommission, DateTime dateStart)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                var oldCard = context.CardsTrancport.All().FirstOrDefault(x => x.CommissionTransactionId == idCommission);
                if (oldCard == null)
                {
                    var newCard = new CardTrancport()
                    {
                        CommissionTransactionId = idCommission,
                        StatusCardTrancport = context.StatusesCardTrancport.All().FirstOrDefault(),
                        DateStart = dateStart
                    };
                    context.CardsTrancport.AddOrUpdate(newCard);
                    context.SaveChanges();
                    return newCard.Id;
                }
                return oldCard.Id;
            }
        }

        public void DeleteCardTrancport(int idCommission)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                var dleteCard = context.CardsTrancport.All().FirstOrDefault(x => x.CommissionTransactionId == idCommission);
                if (dleteCard != null)
                {
                    context.CardsTrancport.Delete(dleteCard);
                    context.SaveChanges();
                }
            }
        }

        public IEnumerable<CardTrancport> GetCardTrancports(User user, params Expression<Func<CardTrancport, object>>[] includes)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                if (user.IsAdmin())
                    return context.CardsTrancport
                        .All(includes)
                        .ToList();
                    
                return context.CardsTrancport
                        .All(includes)
                        .Where(x => x.CommissionTransaction.User.Id == user.Id)
                        .ToList();
            }
        }

        public CardTrancportDocument GetDocument(int id)
        {
            using (var context = IoC.Resolve<IDataContext>())
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

        public IEnumerable<StatusCardTrancport> GetStatusesCardTrancports()
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                return context.StatusesCardTrancport.All().ToList();
            }
        }

        public void SaveDocument(CardTrancportDocument document)
        {
            //if (document.UserId == 0)
            //    throw new ArgumentException("UserId");

            using (var context = IoC.Resolve<IDataContext>())
            {
                var firstCardTrancport = document.CardTrancport;
                if (firstCardTrancport.Id == 0)
                {

                    firstCardTrancport.CommissionTransaction.UserId = document.UserId;
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

                //lock (_sync)
                //{
                //    firstCardTrancport.CommissionTransaction.Number = _sequnce.CurrentValue(firstCardTrancport.CommissionTransaction.Date);
                    context.SaveChanges();
                //    _sequnce.NextValue(firstCardTrancport.CommissionTransaction.Date);
                //}

                //document.Id = firstCardTrancport.CommissionTransaction.Id;
            }
        }
    }
}
