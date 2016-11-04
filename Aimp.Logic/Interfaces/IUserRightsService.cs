using System;
using System.Linq.Expressions;
using Aimp.Entities;
using Entities;
using System.Collections.Generic;

namespace Aimp.Logic.Interfaces
{
    public interface IUserRightsService
    {
        IEnumerable<UserRight> GetUserRights(int id);
        IEnumerable<User> GetUsers(Expression<Func<User, bool>> predicate = null);
        void SaveUser(IEnumerable<string> rightIds, User user);
        void DeleteUser(User user);
    }
}
