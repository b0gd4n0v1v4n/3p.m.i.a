using Aimp.Entities;
using System.Collections.Generic;

namespace Aimp.ServiceContracts.CreditTransactions
{
    public class CreditTransactionInfoDto
    {
        public IEnumerable<ICreditor> Creditors { get; set; }
        public IEnumerable<IRequisit> Requisits { get; set; }
    }
}