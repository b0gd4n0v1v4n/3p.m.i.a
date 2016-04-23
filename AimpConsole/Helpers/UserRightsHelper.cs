using System.Collections.Generic;
using System.Linq;
using Models.Entities;
using System;
using AimpLogic.Logic;
using AimpLogic.UserRights;

namespace AimpConsole.Helpers
{
    public class UserRightsHelper : AimpHelper,IDisposable
    {
        private IUserRightsService _logic;
        public SystemUser GetUser()
        {
            return _logic.User;
        }
        public UserRightsHelper()
        {
            _logic = new UserRightsService(User.Login, User.Password);
        }
        public IEnumerable<UserRight> GetUserRights(int id)
        {
            return _logic.GetUserRights(id).ToList();
        }
        public IEnumerable<User> GetUsers()
        {
            return _logic.GetUsers().ToList();
        }

        public void SaveUser(User user, IEnumerable<string> rightIds)
        {
            _logic.SaveUser(rightIds,user);
        }

        public void DeleteUser(User user)
        {
            _logic.DeleteUser(user);
        }

        public void Dispose()
        {
            _logic?.Dispose();
        }
    }
}
