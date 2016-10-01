namespace Aimp.Entities
{
    public interface IBankReportClient : IEntity
    {
        bool Used { get; set; }
        int BankId { get; set; }
        IBank Bank { get; set; }
        int BankStatusId { get; set; }
        IBankStatus BankStatus { get; set; }
        int ClientReportId { get; set; }
        IClientReport ClientReport { get; set; }
    }
}
