using Models.Entities;
using System.Collections.Generic;

namespace Models.ContractorInfo
{
    public class SearchContractorResult : IResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public IEnumerable<Contractor> Contractors { get; set; }
    }
}
