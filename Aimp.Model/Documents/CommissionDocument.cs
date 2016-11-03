using AIMP_v3._0.Model;

using System;
using Entities;

namespace Aimp.Model.Documents
{
    public class CommissionDocument : Identity, IDocument
    {
        public DocumentType DocumentType { get { return DocumentType.Commission; } }
        public string Identity { get; set; }
        public bool IsNew { get; set; }
        public DateTime Date { get; set; }
        public int Number { get; set; }
        public Contractor Seller { get; set; }
        public Contractor Owner { get; set; }
        public Trancport Trancport { get; set; }
        public decimal Price { get; set; }
        public DateTime? DateProxy { get; set; }
        public string NumberProxy { get; set; }
        public string NumberRegistry { get; set; }
        public decimal Commission { get; set; }

        public decimal Parking { get; set; }

        public bool IsTwoMounth { get; set; }
        public bool IsUseCardTrancport { get; set; }
        public string Manager { get; set; }
        public virtual SourceTrancport SourceTrancport { get; set; }
        public string CountKey { get; set; }
        public string ComplectDoc { get; set; }
        public decimal PriceDkp { get; set; }
        public string Mileage { get; set; } //пробег
        public int UserId { get; set; }
    }
}
