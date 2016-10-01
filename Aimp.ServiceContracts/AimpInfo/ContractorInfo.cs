using Aimp.Entities;
using System.Collections.Generic;

namespace Aimp.ServiceContracts.AimpInfo
{
    public class ContractorInfo
    {
        public IEnumerable<ICity> Cities { get; set; }
        public IEnumerable<IRegion> Regions { get; set; }
    }
}
