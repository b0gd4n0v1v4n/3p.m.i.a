using Aimp.Model.Entities;
using System.Collections.Generic;

namespace Aimp.Model.ContractorInfo
{
    public class SearchContractorResult : IResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public IEnumerable<Contractor> Contractors { get; set; }
    }
}
