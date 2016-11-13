using System;
using System.Collections.Generic;
using Aimp.Model.Documents;
using Entities;
using System.Linq.Expressions;

namespace Aimp.Logic.Interfaces
{
    public interface ICardTrancportService
    {
        CardTrancportDocument GetDocument(int id);
        int AddCardTrancport(int idCommission, DateTime dateStart);
        void SaveDocument(CardTrancportDocument document);
        void DeleteCardTrancport(int id);
        IEnumerable<CardTrancport> GetCardTrancports(User user, params Expression<Func<CardTrancport, object>>[] includes);
        IEnumerable<StatusCardTrancport> GetStatusesCardTrancports();
    }
}