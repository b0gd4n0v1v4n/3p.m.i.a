using System.Collections.Generic;

namespace Aimp.UserRights
{
    public interface IAccount
    {
        int Id { get; }
        IEnumerable<string> RightIds { get; }
    }
}
