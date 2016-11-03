using Aimp.Model.Documents;
using System.Collections.Generic;
using Entities;

namespace Aimp.Model.ReportOfClient
{
    public class ClientReport {

        public ClientReportDocument Document { get; set; } 
        
        public IEnumerable<ClientStatus> ClientStatuses { get; set; }

        public IEnumerable<CreditProgramm> CreditProgramms { get; set; }

        public IEnumerable<Bank> Banks { get; set; }

        public IEnumerable<BankStatus> BankStatuses { get; set; }
    }
}
