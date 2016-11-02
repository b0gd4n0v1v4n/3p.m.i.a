using System.Collections.Generic;
using Aimp.Model.Entities;

namespace Aimp.Model.TrancportInfo
{
    public class SearchTrancportResult : IResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public IEnumerable<Trancport> Trancports { get; set; }
    }
}
