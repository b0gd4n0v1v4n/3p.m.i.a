using Aimp.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aimp.Model.Entities
{
    public class BankReportClient : Entity, IBankReportClient
    {
        public bool Used { get; set; }
        [ForeignKey("Bank")]
        public int BankId { get; set; }
        public virtual IBank Bank { get; set; }
        [ForeignKey("BankStatus")]
        public int BankStatusId { get; set; }
        public IBankStatus BankStatus { get; set; }
        [ForeignKey("ClientReport")]
        public int ClientReportId { get; set; }
        public IClientReport ClientReport { get; set; }
    }
}
