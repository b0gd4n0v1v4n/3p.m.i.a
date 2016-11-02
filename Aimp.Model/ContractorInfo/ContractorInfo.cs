using Aimp.Model.Entities;
using System.Collections.Generic;

namespace Aimp.Model.ContractorInfo
{
    public class ContractorInfo : IResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<Region> Regions { get; set; }
    }
}
