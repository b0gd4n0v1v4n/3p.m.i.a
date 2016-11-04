using AIMP_v3._0.Model;
using Entities;
using System.Collections.Generic;

namespace AIMP_v3._0.ViewModel.ClientOfReport
{
    public class ClientBankStatusViewModel : Identity
    {
        public bool Enable { get; set; }

        public Bank Bank { get; set; }

        public BankStatus SelectedBankStatus { get; set; }  

        public IEnumerable<BankStatus> BankStatuses { get; set; }
    }
}
