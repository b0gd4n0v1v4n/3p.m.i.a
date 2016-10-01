using System;

namespace Aimp.Entities
{
    public interface ICreditTransaction : ITransactionProxy
    {
         int? AgentDocumentId { get; set; }
         IUserFile AgentDocument { get; set; }
         int? DkpDocumentId { get; set; }
         IUserFile DkpDocument { get; set; }
         DateTime? DateAgent { get; set; }
         DateTime? DateDkp { get; set; }
         decimal PriceBank { get; set; }
         decimal DownPayment { get; set; }
         decimal CreditSumm { get; set; }
         decimal RealPrice { get; set; }
         decimal DownPaymentCashbox { get; set; }
         int? CreditorId { get; set; }
         ICreditor Creditor { get; set; }
         int RequisitId { get; set; }
         IRequisit Requisit { get; set; }
         decimal ReportInsurance { get; set; }
         decimal Rollback { get; set; }
         string Source { get; set; }
         bool IsCredit { get; set; }
         decimal CommissionCashbox { get; set; }
    }
}
