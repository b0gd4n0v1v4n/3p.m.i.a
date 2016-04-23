using System.Collections.Generic;
using Models.Documents;
using Models.Entities;

namespace Models.Commission
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
