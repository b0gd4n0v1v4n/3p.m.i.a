using Aimp.ServiceContracts.TrancportCards;
using System;
using Aimp.Entities;
using System.Collections.Generic;

namespace AIMP_v3._0.Aimp.Services.Clients
{
    public class TrancportCardsClient : ITrancportCardsContract, IDisposable
    {
        public int AddCardTrancport(int idCommission, DateTime dateStart)
        {
            throw new NotImplementedException();
        }

        public void DeleteCardTrancport(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public CardTrancportsDto GetCardsTrancport()
        {
            throw new NotImplementedException();
        }

        public CardTrancportDocument GetCardTrancport(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IStatusCardTrancport> GetStatusesCardTrancport()
        {
            throw new NotImplementedException();
        }

        public void SaveCardTrancport(CardTrancportDocument document)
        {
            throw new NotImplementedException();
        }
    }
}
