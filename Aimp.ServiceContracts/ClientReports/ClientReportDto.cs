using Aimp.Entities;
using System.Collections.Generic;

namespace Aimp.ServiceContracts.ClientReports
{
    public class ClientReportDto
    {
        public IEnumerable<IBankReportClient> BankReportClients { get; set; }

        public IEnumerable<IClientStatus> ClientStatuses { get; set; }

        public IEnumerable<ICreditProgramm> CreditProgramms { get; set; }

        public IEnumerable<IBank> Banks { get; set; }

        public IEnumerable<IBankStatus> BankStatuses { get; set; }
    }
}
