using Models.Documents;
using Models.Entities;
using System.Collections.Generic;

namespace Models.CreditTransact
{
    public class CreditTransactionDto : IResponse
    {
        public CreditTransactionDocument Document { get; set; }
        
        public bool Error { get; set; }
        public string Message { get; set; }
    }
}
