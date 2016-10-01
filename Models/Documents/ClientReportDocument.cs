using Models.Entities;
using System.Collections.Generic;
using System;

namespace Aimp.ServiceContracts.ClientReports
{
    public class ClientReportDocument
    {
        public IEnumerable<BankReportClient> BankReportClients { get; set; }
        public int Id { get; set; }
        public bool IsNew { get { return Id == 0; } }
    }
}
