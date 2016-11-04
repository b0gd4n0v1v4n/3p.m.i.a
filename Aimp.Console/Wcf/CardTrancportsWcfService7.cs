using System;
using System.Collections.Generic;
using System.Linq;
using Aimp.Domain;
using Aimp.Logic.Interfaces;
using Aimp.Model.CardTrancports;
using Aimp.Model.Documents;
using Aimp.ServiceContract.Services;
using Entities;

namespace Aimp.Console.Wcf
{
    public class CardTrancportsWcfService7 :CashTransactionWcfService6, ICardTrancportsWcfService
    {
        public CardTrancportsDto GetCardsTrancport()
        {
            EventLog($"Get cards trancport");
            try
            {
                var service = IoC.Resolve<ICardTrancportService>();

                var items = service.GetCardTrancports(CurrentUser)
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
                StatusesCardForFilerStart = new string[] { service.GetStatusesCardTrancports().FirstOrDefault()?.Name }
            };
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public CardTrancportDocument GetCardTrancport(int id)
        {
            EventLog($"Get card trancport id: {id}");
            try
            {
                return IoC.Resolve<ICardTrancportService>().GetDocument(id);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public int AddCardTrancport(int idCommission, DateTime dateStart)
        {
            EventLog($"Add card trancport for commission id: {idCommission}");
            try
            {
                return IoC.Resolve<ICardTrancportService>().AddCardTrancport(idCommission, dateStart);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public int SaveCardTrancport(CardTrancportDocument document)
        {
            EventLog($"Save card trancport document id: {document.Id}");
            try
            {
                IoC.Resolve<ICardTrancportService>().SaveDocument(document);
                return document.CardTrancport.Id;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public void DeleteCardTrancport(int idCommission)
        {
            EventLog($"Delete card trancport commission id: {idCommission}");
            try
            {
                IoC.Resolve<ICardTrancportService>().DeleteCardTrancport(idCommission);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public IEnumerable<StatusCardTrancport> GetStatusesCardTrancport()
        {
            EventLog($"Get statuses card trancport");
            try
            {
                return IoC.Resolve<ICardTrancportService>().GetStatusesCardTrancports().ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }
    }
}
