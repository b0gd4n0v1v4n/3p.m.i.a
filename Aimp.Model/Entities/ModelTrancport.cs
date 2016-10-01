using Aimp.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aimp.Model.Entities
{
    public class ModelTrancport : Entity, IModelTrancport
    {
        public string Name { get; set; }
        [ForeignKey("Make")]
        public int MakeId { get; set; }
        public IMakeTrancport Make { get; set; }
    }
}