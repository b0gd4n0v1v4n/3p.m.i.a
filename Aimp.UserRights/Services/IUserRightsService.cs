using Aimp.Entities;
using System.Collections.Generic;

namespace Aimp.UserRights.Services
{
    public interface IUserRightsService
    {
         IAccount Auth(string login, string password); 
         IEnumerable<IUserRight> GetUserRights(int id);
         IEnumerable<IUser> GetUsers();
         void SaveUser(IEnumerable<string> rightIds, IUser user);
         void DeleteUser(IUser user);
    }
}
