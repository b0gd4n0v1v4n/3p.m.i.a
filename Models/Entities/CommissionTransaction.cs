using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class CommissionTransaction : TransactionProxy
    {
        public decimal Commission { get; set; }

        public decimal Parking { get; set; }

        public bool IsTwoMounth { get; set; }
        public bool IsUseCardTrancport { get; set; }
        public string Manager { get; set; }
        [ForeignKey("SourceTrancport")]
        public int? SourceTrancportId { get; set; }
        public virtual SourceTrancport SourceTrancport { get; set; }
        public string CountKey { get; set; }
        public string ComplectDoc { get; set; }
        public decimal PriceDkp { get; set; }
        public string Mileage { get; set; } //пробег
    }
}
