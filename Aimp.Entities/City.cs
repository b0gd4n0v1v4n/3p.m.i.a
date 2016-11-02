using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class City : Entity
    {
        public string Name { get; set; }
        [ForeignKey("Region")]
        public int RegionId { get; set; }
        public Region Region { get; set; }
    }
}
