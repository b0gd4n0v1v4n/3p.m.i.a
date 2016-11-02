using System.Collections.Generic;
using Aimp.Model.Documents;
using Aimp.Model.Entities;

namespace Aimp.Model.Commission
{
    public class CommissionDto : IResponse
    {
        public CommissionDocument Document { get; set; }
        public IEnumerable<SourceTrancport> SourcesTrancport { get; set; }
        public IEnumerable<KeyValue<string,string>> PrintedDocuments { get; set; } 
        public bool Error { get; set; }
        public string Message { get; set; }
    }
}
