using Aimp.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aimp.Model.Entities
{
    public class City : Entity, ICity
    {
        public string Name { get; set; }
        [ForeignKey("Region")]
        public int RegionId { get; set; }
        public IRegion Region { get; set; }
    }
}
