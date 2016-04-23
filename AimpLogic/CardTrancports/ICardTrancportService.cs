using Models.Documents;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AimpLogic.CardTrancports
{
    public interface ICardTrancportService : IDisposable
    {
        CardTrancportDocument GetDocument(int id);
        int AddCardTrancport(int idCommission);
        void SaveDocument(CardTrancportDocument document);
        void DeleteCardTrancport(int id);
        IQueryable<CardTrancport> GetCardTrancports();
        IQueryable<StatusCardTrancport> GetStatusesCardTrancports();
    }
}
