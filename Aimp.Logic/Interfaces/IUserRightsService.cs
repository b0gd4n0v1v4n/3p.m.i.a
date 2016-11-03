using Entities;
using System.Collections.Generic;
using System.Linq;

namespace Aimp.Logic.Interfaces
{
    public interface IUserRightsService
    {
        IQueryable<UserRight> GetUserRights(int id);
        IQueryable<User> GetUsers();
        int SaveUser(IEnumerable<string> rightIds, User user);
        void DeleteUser(User user);
    }
}
