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
                DateStart =x.CommissionTransaction.Date,
                Status = x.StatusCardTrancport.Name,
                Number = x.Number.ToString(),
                NumberT = x.NumberT.ToString(),
                MakeModelTrancport = x.MakeModel,
                ColorTrancport = x.CommissionTransaction.Trancport.Color,
                Vin = x.CommissionTransaction.Trancport.Vin,
                Pts = x.CommissionTransaction.ComplectDoc,
                Keys = x.CommissionTransaction.CountKey,
                Price = x.CommissionTransaction.Price.ToString(),
                Manager = x.ManagerSeller,
                OwnerAndTelefon = x.CommissionTransaction.Owner.LastName + " " + x.CommissionTransaction.Owner.FirstName +" "+x.CommissionTransaction.Owner.MiddleName +"/"+ x.CommissionTransaction.Owner.Telefon,
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
        public int AddCard(int idCommission)
        {
           return _logic.AddCardTrancport(idCommission);
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
