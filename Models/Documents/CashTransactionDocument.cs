using AIMP_v3._0.Model;
using Models.Entities;
using System;

namespace Models.Documents
{
    public class CashTransactionDocument : Identity, IDocument
    {
        public DocumentType DocumentType { get { return DocumentType.CashTransaction; } }
        public string Identity { get; set; }
        public bool IsNew { get; set; }
        public DateTime Date { get; set; }
        public int Number { get; set; }
        public Contractor Seller { get; set; }
        public Contractor Buyer { get; set; }
        public Contractor Owner { get; set; }
        public Trancport Trancport { get; set; }
        public decimal Price { get; set; }
        public DateTime? DateProxy { get; set; }
        public string NumberProxy { get; set; }
        public string NumberRegistry { get; set; }
        public int UserId { get; set; }
    }
}
