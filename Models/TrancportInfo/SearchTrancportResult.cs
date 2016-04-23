using System.Collections.Generic;
using Models.Entities;

namespace Models.TrancportInfo
{
    public class SearchTrancportResult : IResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public IEnumerable<Trancport> Trancports { get; set; }
    }
}
