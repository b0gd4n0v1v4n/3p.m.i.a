using System;
namespace Aimp.Entities
{
     public interface IClientReport : IEntity
    {
         DateTime Date { get; set; }
         string Source { get; set; }
         string FullName { get; set; }
         string Trancport { get; set; }
         decimal Price { get; set; }
         decimal? TotalContribution { get; set; }
         int ClientStatusId { get; set; }      
         IClientStatus ClientStatus { get; set; }
         string Telefon { get; set; }
         int CreditProgrammId { get; set; }
         ICreditProgramm CreditProgramm { get; set; }
         decimal? CreditSum { get; set; }
         bool CommissionKnow { get; set; }
         decimal? CommissionRemoval { get; set; }
         bool CommissionCredit { get; set; }
         decimal? ActAssessment { get; set; }
         string DKP_DK { get; set; }
         string Comment { get; set; }
         string CommissionSalon { get; set; }
         int UserId { get; set; }
         IUser User { get; set; }
    }
}
