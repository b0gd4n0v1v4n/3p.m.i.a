using System.Collections.Generic;
using Entities;

namespace Aimp.Model.ReportOfClient
{
    public class ClientReports
    {
        public string UserLastNameForFilter { get; set; }
        public IEnumerable<string> ClientStatusesForFilter { get; set; }
        public IEnumerable<Bank> Banks { get; set; }
        
        public IEnumerable<BankReportClient> Items { get; set; } 
    }
}
