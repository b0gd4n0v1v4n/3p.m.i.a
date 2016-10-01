using Aimp.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aimp.Model.Entities
{
    public class CommissionTransaction : TransactionProxy, ICommissionTransaction
    {
        public decimal Commission { get; set; }

        public decimal Parking { get; set; }

        public bool IsTwoMounth { get; set; }
        public bool IsUseCardTrancport { get; set; }
        public string Manager { get; set; }
        [ForeignKey("SourceTrancport")]
        public int? SourceTrancportId { get; set; }
        public virtual ISourceTrancport SourceTrancport { get; set; }
        public string CountKey { get; set; }
        public string ComplectDoc { get; set; }
        public decimal PriceDkp { get; set; }
        public string Mileage { get; set; } //пробег
    }
}
