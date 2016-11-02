using AIMP_v3._0.Model;
using Aimp.Model.Entities;
using System;

namespace Aimp.Model.Documents
{
    public class CreditTransactionDocument : Identity,IDocument
    {
        public DocumentType DocumentType
        {
            get
            {
                return DocumentType.CreditTransaction;
            }
        }
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
        public virtual UserFile AgentDocument { get; set; }
        public virtual UserFile DkpDocument { get; set; }
        public DateTime? DateAgent { get; set; }
        public DateTime? DateDkp { get; set; }
        public decimal PriceBank { get; set; }
        public decimal DownPayment { get; set; }
        public decimal CreditSumm { get; set; }
        public decimal RealPrice { get; set; }
        public decimal DownPaymentCashbox { get; set; }
        public virtual Creditor Creditor { get; set; }
        public virtual Requisit Requisit { get; set; }
        public decimal ReportInsurance { get; set; }
        public decimal Rollback { get; set; }
        public string Source { get; set; }
        public bool IsCredit { get; set; }
        public decimal CommissionCashbox { get; set; }

        public int UserId { get; set; }
    }
}
