using Aimp.Entities;
using Aimp.ServiceContracts;

using System.Collections.Generic;

namespace AIMP_v3._0.ViewModel.ClientOfReport
{
    public class ClientBankStatusViewModel : Identity
    {
        public bool Enable { get; set; }

        public IBank Bank { get; set; }

        public IBankStatus SelectedBankStatus { get; set; }  

        public IEnumerable<IBankStatus> BankStatuses { get; set; }
    }
}
