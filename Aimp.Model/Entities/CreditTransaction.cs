using Aimp.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aimp.Model.Entities
{
    public class CreditTransaction : TransactionProxy, ICreditTransaction
    {
        [ForeignKey("AgentDocument")]
        public int? AgentDocumentId { get; set; }
        public virtual IUserFile AgentDocument { get; set; }
        [ForeignKey("DkpDocument")]
        public int? DkpDocumentId { get; set; }
        public virtual IUserFile DkpDocument { get; set; }

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
        public virtual ICreditor Creditor { get; set; }
        [ForeignKey("Requisit")]
        public int RequisitId { get; set; }
        public virtual IRequisit Requisit { get; set; }

        [DisplayName("отчёт_по_страховым")]
        public decimal ReportInsurance { get; set; }

        public decimal Rollback { get; set; }

        public string Source { get; set; }

        public bool IsCredit { get; set; }

        public decimal CommissionCashbox { get; set; }
    }
}
