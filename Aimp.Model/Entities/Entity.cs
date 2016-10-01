using Aimp.Entities;
using System.ComponentModel.DataAnnotations;

namespace Aimp.Model.Entities
{
    public abstract class Entity : IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
