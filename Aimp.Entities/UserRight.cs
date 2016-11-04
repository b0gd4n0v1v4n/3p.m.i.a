using System.ComponentModel.DataAnnotations.Schema;
using Entities;

namespace Aimp.Entities
{
    public class UserRight : Entity
    {
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public string RightId { get; set; }
    }
}
