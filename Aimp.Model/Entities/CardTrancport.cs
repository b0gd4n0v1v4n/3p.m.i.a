using Aimp.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aimp.Model.Entities
{
    public class CardTrancport : Entity, ICardTrancport
    {
        public bool Commission { get; set; }
        public int? Number { get; set; }
        public int? NumberT { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateSale { get; set; }
        [ForeignKey("StatusCardTrancport")]
        public int? StatusCardTrancportId { get; set; }
        public virtual IStatusCardTrancport StatusCardTrancport { get; set; }
        public string ContactOne { get; set; }
        public string ContactTwo { get; set; }
        public string ContactThree { get; set; }
        public string ContactFour { get; set; }
        public string ContactFive { get; set; }
        public string ContactOther { get; set; }
        public decimal? PriceTrancportForSeller { get; set; }
        public decimal? CommissionForSeller { get; set; }
        public decimal? HandsForSeller { get; set; }
        public decimal? Sale { get; set; }
        public decimal? Deposit { get; set; }
        public decimal? MREO { get; set; }
        public decimal? Price { get; set; }
        public decimal? TotalInSeller { get; set; }
        public string MakeModel { get; set; }
        public string ManagerSeller { get; set; }
        public string Comment { get; set; }
        public string Rubber { get; set; } //комплект резины
        public string ServiceBook { get; set; }
        public string ServiceComment { get; set; }
        [ForeignKey("CommissionTransaction")]
        public int CommissionTransactionId { get; set; }
        public virtual ICommissionTransaction CommissionTransaction { get; set; }
        public bool TradeIn { get; set; }
    }
}
