using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public abstract class Entity : IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
