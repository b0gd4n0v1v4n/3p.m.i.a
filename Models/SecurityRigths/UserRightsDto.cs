using System.Collections.Generic;
using Models.Entities;

namespace Models.SecurityRigths
{
    public class UserRightsDto : IResponse
    {
        public IEnumerable<UserRight> UserRights { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
    }
}