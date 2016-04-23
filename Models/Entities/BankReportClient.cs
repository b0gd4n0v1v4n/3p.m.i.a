using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class BankReportClient : Entity
    {
        public bool Used { get; set; }
        [ForeignKey("Bank")]
        public int BankId { get; set; }
        public virtual Bank Bank { get; set; }
        [ForeignKey("BankStatus")]
        public int BankStatusId { get; set; }
        public BankStatus BankStatus { get; set; }
        [ForeignKey("ClientReport")]
        public int ClientReportId { get; set; }
        public ClientReport ClientReport { get; set; }
    }
}
