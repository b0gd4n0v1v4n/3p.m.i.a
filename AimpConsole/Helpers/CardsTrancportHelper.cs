using AimpLogic.CardTrancports;
using Models.CardTrancports;
using Models.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AimpConsole.Helpers
{
    public class CardsTrancportHelper : AimpHelper, IDisposable
    {
        private ICardTrancportService _logic;
        public void Dispose()
        {
            _logic?.Dispose();
        }
        public CardsTrancportHelper()
        {
            _logic = new CardTrancportService(User.Login, User.Password);
        }
        public CardTrancportsDto GetCards()
        {
            var items = _logic.GetCardTrancports()
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
                StatusesCardForFilerStart = new string[] { _logic.GetStatusesCardTrancports().FirstOrDefault()?.Name }
            };
        }
        public StatusesCardTrancportDto GetStatusesCard()
        {
            return new StatusesCardTrancportDto()
            {
                Items = _logic.GetStatusesCardTrancports().ToList()
            };
        }
        public CardTrancportDto GetCard(int id)
        {
            var document = _logic.GetDocument(id);
            return new CardTrancportDto()
            {
                Document = document
            };
        }
        public int AddCard(int idCommission,DateTime dateStart)
        {
           return _logic.AddCardTrancport(idCommission, dateStart);
        }
        public void DeleteCard(int id)
        {
            _logic.DeleteCardTrancport(id);
        }
        public void SaveCard(CardTrancportDocument document)
        {
            _logic.SaveDocument(document);
        }
    }
}
