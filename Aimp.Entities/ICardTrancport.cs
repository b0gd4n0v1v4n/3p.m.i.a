using System;

namespace Aimp.Entities
{
   public interface ICardTrancport : IEntity
    {
        bool Commission { get; set; }
        int? Number { get; set; }
        int? NumberT { get; set; }
        DateTime? DateStart { get; set; }
        DateTime? DateSale { get; set; }
        int? StatusCardTrancportId { get; set; }
        IStatusCardTrancport StatusCardTrancport { get; set; }
        string ContactOne { get; set; }
        string ContactTwo { get; set; }
        string ContactThree { get; set; }
        string ContactFour { get; set; }
        string ContactFive { get; set; }
        string ContactOther { get; set; }
        decimal? PriceTrancportForSeller { get; set; }
        decimal? CommissionForSeller { get; set; }
        decimal? HandsForSeller { get; set; }
        decimal? Sale { get; set; }
        decimal? Deposit { get; set; }
        decimal? MREO { get; set; }
        decimal? Price { get; set; }
        decimal? TotalInSeller { get; set; }
        string MakeModel { get; set; }
        string ManagerSeller { get; set; }
        string Comment { get; set; }
        string Rubber { get; set; } //комплект резины
        string ServiceBook { get; set; }
        string ServiceComment { get; set; }
        int CommissionTransactionId { get; set; }
        ICommissionTransaction CommissionTransaction { get; set; }
        bool TradeIn { get; set; }
    }
}
