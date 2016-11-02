using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class CreditTransaction : TransactionProxy
    {
        [ForeignKey("AgentDocument")]
        public int? AgentDocumentId { get; set; }
        public virtual UserFile AgentDocument { get; set; }
        [ForeignKey("DkpDocument")]
        public int? DkpDocumentId { get; set; }
        public virtual UserFile DkpDocument { get; set; }

        public DateTime? DateAgent { get; set; }

        public DateTime? DateDkp { get; set; }

        public decimal PriceBank { get; set; }

        [DisplayName("Первый взнос")]
        public decimal DownPayment { get; set; }

        public decimal CreditSumm { get; set; }

        public decimal RealPrice { get; set; }

        public decimal DownPaymentCashbox { get; set; }
        [ForeignKey("Creditor")]
        public int? CreditorId { get; set; }
        public virtual Creditor Creditor { get; set; }
        [ForeignKey("Requisit")]
        public int RequisitId { get; set; }
        public virtual Requisit Requisit { get; set; }

        [DisplayName("отчёт_по_страховым")]
        public decimal ReportInsurance { get; set; }

        public decimal Rollback { get; set; }

        public string Source { get; set; }

        public bool IsCredit { get; set; }

        public decimal CommissionCashbox { get; set; }
    }
}
