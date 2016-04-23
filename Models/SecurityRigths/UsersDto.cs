using System.Collections.Generic;
using Models.Entities;

namespace Models.SecurityRigths
{
    public class UsersDto : IResponse
    {
        public IEnumerable<User> Users { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
    }
}
