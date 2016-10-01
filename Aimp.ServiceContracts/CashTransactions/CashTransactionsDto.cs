using System.Collections.Generic;

namespace Aimp.ServiceContracts.CashTransactions
{
    public class CashTransactionsDto
    {
        public IEnumerable<CashTransactionListItem> Items { get; set; }
    }
}
