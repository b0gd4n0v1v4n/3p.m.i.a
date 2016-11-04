using System;
using System.Collections.Generic;
using System.Linq;
using Aimp.Domain;
using Aimp.Entities;
using Aimp.Logic.Interfaces;
using Aimp.Model;
using Aimp.ServiceContract.Services;
using Entities;

namespace Aimp.Console.Wcf
{
    public class AimpUsersWcfService8 : CardTrancportsWcfService7,IAimpUsersWcfService
    {
        public IEnumerable<UserRight> GetUserRights(int id)
        {
            EventLog($"Get user rights");
            try
            {
                return IoC.Resolve<IUserRightsService>().GetUserRights(id).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public AimpUserDto Auth()
        {
            EventLog("Auth");
            try
            {
                var userRights = IoC.Resolve<IUserRightsService>().GetUserRights(CurrentUser.Id).Select(x => x.RightId).ToList();

                return new AimpUserDto()
                {
                    Id = CurrentUser.Id,
                    UserRights = userRights
                };
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public IEnumerable<User> GetUsers()
        {
            EventLog("get users");
            try
            {
                return IoC.Resolve<IUserRightsService>().GetUsers().ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public int SaveUser(User user, IEnumerable<string> rightIds)
        {
            EventLog("save user: [{user.Login},{user.Password}]");
            try
            {
                IoC.Resolve<IUserRightsService>().SaveUser(rightIds, user);
                return user.Id;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public void DeleteUser(User user)
        {
            EventLog("delete user: {user.Id}");
            try
            {
                IoC.Resolve<IUserRightsService>().DeleteUser(user);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }
    }
}
