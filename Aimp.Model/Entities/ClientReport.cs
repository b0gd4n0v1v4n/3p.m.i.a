using Aimp.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aimp.Model.Entities
{
    public class ClientReport : Entity, IClientReport
    {
        public DateTime Date { get; set; }
        public string Source { get; set; }
        public string FullName { get; set; }
        public string Trancport { get; set; }
        public decimal Price { get; set; }
        [DisplayName("Общий взнос")]
        public decimal? TotalContribution { get; set; }
        [ForeignKey("ClientStatus")]
        public int ClientStatusId { get; set; }      
        public virtual IClientStatus ClientStatus { get; set; }
        public string Telefon { get; set; }
        [ForeignKey("CreditProgramm")]
        public int CreditProgrammId { get; set; }
        public virtual ICreditProgramm CreditProgramm { get; set; }
        public decimal? CreditSum { get; set; }
        [DisplayName("Комиссия знает")]
        public bool CommissionKnow { get; set; }
        [DisplayName("Комиссия за снятие")]
        public decimal? CommissionRemoval { get; set; }
        [DisplayName("Комиссии в кредите")]
        public bool CommissionCredit { get; set; }
        [DisplayName("Акт оценки")]
        public decimal? ActAssessment { get; set; }
        public string DKP_DK { get; set; }
        public string Comment { get; set; }
        public string CommissionSalon { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual IUser User { get; set; }
    }
}
