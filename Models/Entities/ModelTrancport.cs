using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class ModelTrancport : Entity
    {
        public string Name { get; set; }
        [ForeignKey("Make")]
        public int MakeId { get; set; }
        public MakeTrancport Make { get; set; }
    }
}