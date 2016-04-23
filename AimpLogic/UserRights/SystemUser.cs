using System.Collections.Generic;

namespace AimpLogic.UserRights
{
    public class SystemUser
    {
        public int Id { get; set; }
        public IEnumerable<string> RightIds { get; set; }
    }
}
