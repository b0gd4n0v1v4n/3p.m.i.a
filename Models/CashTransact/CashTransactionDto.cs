using System.Collections.Generic;
using Models.Documents;

namespace Models.CashTransact
{
    public class CashTransactionDto : IResponse
    {
        public CashTransactionDocument Document { get; set; }
        public IEnumerable<KeyValue<string,string>> PrintedDocuments { get; set; } 
        public bool Error { get; set; }
        public string Message { get; set; }
    }
}
