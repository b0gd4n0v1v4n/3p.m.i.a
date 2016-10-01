using Aimp.Entities;
using System.Collections.Generic;
namespace Aimp.ServiceContracts.CommissionTransactions
{
    public class CommissionDto 
    {
        public ICommissionTransaction Document { get; set; }
        public IEnumerable<ISourceTrancport> SourcesTrancport { get; set; }
        public IEnumerable<KeyValue<string,string>> PrintedDocuments { get; set; }
    }
}
