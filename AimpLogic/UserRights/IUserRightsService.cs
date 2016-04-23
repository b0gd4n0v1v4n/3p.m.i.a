using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AimpLogic.UserRights
{
    public interface IUserRightsService :IDisposable
    {
        SystemUser User { get; }
         IQueryable<UserRight> GetUserRights(int id);
         IQueryable<User> GetUsers();
         void SaveUser(IEnumerable<string> rightIds, User user);
         void DeleteUser(User user);
    }
}
