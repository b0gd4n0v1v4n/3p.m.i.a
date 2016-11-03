using System.Collections.Generic;
using Aimp.Model.Documents;
using Entities;

namespace Aimp.Model.Commission
{
    public class CommissionDto 
    {
        public CommissionDocument Document { get; set; }
        public IEnumerable<SourceTrancport> SourcesTrancport { get; set; }
        public IEnumerable<KeyValue<string,string>> PrintedDocuments { get; set; } 
    }
}
