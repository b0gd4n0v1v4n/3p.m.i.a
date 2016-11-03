using System.Collections.Generic;
using Aimp.Model.Documents;

namespace Aimp.Model.CashTransact
{
    public class CashTransactionDto
    {
        public CashTransactionDocument Document { get; set; }
        public IEnumerable<KeyValue<string,string>> PrintedDocuments { get; set; } 
    }
}
