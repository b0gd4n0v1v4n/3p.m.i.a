using Aimp.ServiceContracts;
using System.Collections.Generic;

namespace Aimp.ServiceContracts.CashTransactions
{
    public class CashTransactionDto
    {
        public CashTransactionDocument Document { get; set; }
        public IEnumerable<KeyValue<string,string>> PrintedDocuments { get; set; }
    }
}
