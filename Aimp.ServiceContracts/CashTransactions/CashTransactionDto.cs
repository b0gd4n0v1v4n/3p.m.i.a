using Aimp.Entities;
using Aimp.ServiceContracts;
using System.Collections.Generic;

namespace Aimp.ServiceContracts.CashTransactions
{
    public class CashTransactionDto
    {
        public ICashTransaction Document { get; set; }
        public IEnumerable<KeyValue<string,string>> PrintedDocuments { get; set; }
    }
}
