using Aimp.Entities;
using System.Collections.Generic;

namespace Aimp.ServiceContracts.ClientReports
{
    public class ClientReports
    {
        public string UserLastNameForFilter { get; set; }
        public IEnumerable<string> ClientStatusesForFilter { get; set; }
        public IEnumerable<IBank> Banks { get; set; }
        
        public IEnumerable<ClientReportListItem> Items { get; set; } 
    }
}
