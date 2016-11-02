using Aimp.Model.Documents;
using Aimp.Model.Entities;
using System.Collections.Generic;

namespace Aimp.Model.CreditTransact
{
    public class CreditTransactionDto : IResponse
    {
        public CreditTransactionDocument Document { get; set; }
        
        public bool Error { get; set; }
        public string Message { get; set; }
    }
}
