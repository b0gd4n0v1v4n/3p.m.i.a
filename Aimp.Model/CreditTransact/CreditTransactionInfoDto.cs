using System.Collections.Generic;
using Entities;

namespace Aimp.Model.CreditTransact
{
    public class CreditTransactionInfoDto 
    {
        public IEnumerable<Creditor> Creditors { get; set; }
        public IEnumerable<Requisit> Requisits { get; set; }
    }
}