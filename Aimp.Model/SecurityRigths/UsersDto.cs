using System.Collections.Generic;
using Aimp.Model.Entities;

namespace Aimp.Model.SecurityRigths
{
    public class UsersDto : IResponse
    {
        public IEnumerable<User> Users { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
    }
}
