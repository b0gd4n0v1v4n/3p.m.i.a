using Aimp.Model.Documents;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aimp.Logic.Interfaces
{
    public interface ICardTrancportService
    {
        CardTrancportDocument GetDocument(int id);
        int AddCardTrancport(int idCommission, DateTime dateStart);
        void SaveDocument(CardTrancportDocument document);
        void DeleteCardTrancport(int id);
        IQueryable<CardTrancport> GetCardTrancports(User user);
        IQueryable<StatusCardTrancport> GetStatusesCardTrancports();
    }
}
