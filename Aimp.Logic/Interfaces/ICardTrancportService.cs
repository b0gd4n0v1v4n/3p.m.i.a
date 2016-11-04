using System;
using System.Collections.Generic;
using Aimp.Model.Documents;
using Entities;

namespace Aimp.Logic.Interfaces
{
    public interface ICardTrancportService
    {
        CardTrancportDocument GetDocument(int id);
        int AddCardTrancport(int idCommission, DateTime dateStart);
        void SaveDocument(CardTrancportDocument document);
        void DeleteCardTrancport(int id);
        IEnumerable<CardTrancport> GetCardTrancports(User user);
        IEnumerable<StatusCardTrancport> GetStatusesCardTrancports();
    }
}