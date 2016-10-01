using Aimp.UserRights;
using System.Collections.Generic;

namespace Aimp.Logic.UserRights
{
    public class Accaunt : IAccount
    {
        public int Id { get; set; }
        public IEnumerable<string> RightIds { get; set; }
    }
}
