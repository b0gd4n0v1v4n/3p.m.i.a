using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Documents;
using Models.Entities;
using AimpLogic.Logic;
using AimpLogic.UserRights;
using AimpLogic.Logging;

namespace AimpLogic.CardTrancports
{
    public class CardTrancportService : Aimp, ICardTrancportService
    {
        public CardTrancportService(string login, string password) 
            :base(login,password)
        {

        }

        public int AddCardTrancport(int idCommission)
        {
            try
            {
                CheckAddRight();
                var oldCard = Context.CardsTrancport.All().FirstOrDefault(x => x.CommissionTransactionId == idCommission);
                if (oldCard == null)
                {
                    var newCard = new CardTrancport()
                    {
                        CommissionTransactionId = idCommission,
                        StatusCardTrancport = Context.StatusesCardTrancport.All().FirstOrDefault(),
                    };
                    Context.CardsTrancport.AddOrUpdate(newCard);
                    Context.SaveChanges();
                    return newCard.Id;
                }
                return oldCard.Id;
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);

                throw new Exception("Неудалось добавить карточку ТС");
            }
        }

        public void DeleteCardTrancport(int idCommission)
        {
            try
            {
                CheckDeleteRight();
                var dleteCard = Context.CardsTrancport.All().FirstOrDefault(x => x.CommissionTransactionId == idCommission);
                if (dleteCard != null)
                {
                    Context.CardsTrancport.Delete(dleteCard);
                    Context.SaveChanges();
                }
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);

                throw new Exception("Неудалось удалить карточку ТС");
            }
        }

        public IQueryable<CardTrancport> GetCardTrancports()
        {
            try
            {
                if (IsAdmin())
                    return Context.CardsTrancport
                        .All(x => x.CommissionTransaction.User,x=>x.CommissionTransaction.Trancport.Make, x => x.CommissionTransaction.Trancport.Model,x=>x.CommissionTransaction.Owner.LegalPerson);
                else
                    return Context.CardsTrancport
                        .All(x => x.CommissionTransaction.User, x => x.CommissionTransaction.Trancport.Make, x => x.CommissionTransaction.Trancport.Model, x => x.CommissionTransaction.Owner.LegalPerson)
                        .Where(x => x.CommissionTransaction.User.Id == User.Id);
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);

                throw new Exception("Неудалось получить список");
            }
        }

        public CardTrancportDocument GetDocument(int id)
        {
            try
            {
                CheckViewRight();
                var preChekcs = Context.PreChecksCardTrancport
                                .All(x => x.CardTrancport.CommissionTransaction.SourceTrancport)
                                .Where(x => x.CardTrancport.Id == id)
                                .ToList();

                return new CardTrancportDocument()
                {
                    CardTrancport = Context.CardsTrancport.Get(id,x=>x.StatusCardTrancport,x=>x.CommissionTransaction.Seller,x=>x.CommissionTransaction.SourceTrancport,x=>x.CommissionTransaction.Owner, x => x.CommissionTransaction.Trancport.Make, x => x.CommissionTransaction.Trancport.Model),
                    PreChecks = preChekcs
                };
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);
                throw new Exception("Не удалось получить документ, обратитесь к администратору");
            }
        }

        public IQueryable<StatusCardTrancport> GetStatusesCardTrancports()
        {
            try
            {
                CheckViewRight();
                return Context.StatusesCardTrancport.All();
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);
                throw new Exception("Не удалось список статусов карт, обратитесь к администратору");
            }
        }

        public void SaveDocument(CardTrancportDocument document)
        {
            try
            {
                CheckAddRight();
                var firstCardTrancport = document.CardTrancport;
                if (firstCardTrancport.Id == 0)
                {

                    firstCardTrancport.CommissionTransaction.UserId = User.Id;
                    foreach (var iPreCheks in document.PreChecks)
                    {
                        iPreCheks.CardTrancport = firstCardTrancport;

                        Context.PreChecksCardTrancport.AddOrUpdate(iPreCheks);
                    }
                }
                else
                {
                   

                    var oldPreCheks = Context.PreChecksCardTrancport
                                               .All()
                                               .Where(x => x.CardTrancport.Id == firstCardTrancport.Id)
                                               .ToList();
                    foreach (var iPreCheck in oldPreCheks)
                    {
                        var  preCheck =
                            document.PreChecks.FirstOrDefault(
                                x => x.Id == iPreCheck.Id);
                        if (preCheck == null)
                            Context.PreChecksCardTrancport.Delete(iPreCheck);
                        else
                            Context.PreChecksCardTrancport.AddOrUpdate(preCheck);
                    }
                    foreach (var iNewPreCheck in document.PreChecks.Where(x => x.Id == 0))
                    {
                        Context.PreChecksCardTrancport.AddOrUpdate(iNewPreCheck);
                    }
                }
                Context.CardsTrancport.AddOrUpdate(firstCardTrancport);
                Context.SaveChanges();
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);
                throw new Exception("Не удалось сохранить документ, обратитесь к администратору");
            }
        }
    }
}
