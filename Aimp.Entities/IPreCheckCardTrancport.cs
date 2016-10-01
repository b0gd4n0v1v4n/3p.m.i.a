using System;

namespace Aimp.Entities
{
    public interface IPreCheckCardTrancport : IEntity
    {
         DateTime Date { get; set; }
         string Name { get; set; }
         decimal Summ { get; set; }
         decimal PriceForClient { get; set; }
         bool Paid { get; set; }
         bool Card { get; set; }
         int CardTrancportId { get; set; }
         ICardTrancport CardTrancport { get; set; }
    }
}
