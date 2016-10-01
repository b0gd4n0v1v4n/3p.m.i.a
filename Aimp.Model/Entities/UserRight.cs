using Aimp.Infrastructure.Users;
using Aimp.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aimp.Model.Entities
{
    public class UserRight : Entity, IUserRight
    {
        [ForeignKey("User")]
        public int UserId { get; set; }
        public Infrastucture.Entities.IUser User { get; set; }
        public string RightId { get; set; }
    }
}
