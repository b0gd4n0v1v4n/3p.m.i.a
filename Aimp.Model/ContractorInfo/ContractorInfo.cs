using Entities;
using System.Collections.Generic;

namespace Aimp.Model.ContractorInfo
{
    public class ContractorInfo
    {
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<Region> Regions { get; set; }
    }
}
