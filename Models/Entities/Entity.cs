using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public abstract class Entity : IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
