using System.Collections.Generic;
using Aimp.Model.Entities;

namespace Aimp.Model.SecurityRigths
{
    public class UserRightsDto : IResponse
    {
        public IEnumerable<UserRight> UserRights { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
    }
}