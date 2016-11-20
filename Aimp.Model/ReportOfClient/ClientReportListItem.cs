using AIMP_v3._0.Model;
using System;

namespace Aimp.Model.ReportOfClient
{
    public class ClientReportListItem
        : Identity
    {
        public DateTime DateReportClient { get; set; }
        public string FullNameReportClient { get; set; }
        public string TelefonReportClient { get; set; }
        public string TrancportNameReportClient { get; set; }
        public string PriceTrancportReportClient { get; set; }
        public string TotalContributionReportClient { get; set; }
        public string ProgrammCreditReportClient { get; set; }
        public string[] BankStatusesReportClient { get; set; }
        public string ClientStatusReportClient { get; set; }
        public string SourceInfoReportClient { get; set; }
        public string ManagerReportClient { get; set; }
    }
}
