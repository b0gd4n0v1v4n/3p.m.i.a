using System.Collections.Generic;
using Aimp.Model.Documents;

namespace Aimp.Model.CashTransact
{
    public class CashTransactionDto : IResponse
    {
        public CashTransactionDocument Document { get; set; }
        public IEnumerable<KeyValue<string,string>> PrintedDocuments { get; set; } 
        public bool Error { get; set; }
        public string Message { get; set; }
    }
}
