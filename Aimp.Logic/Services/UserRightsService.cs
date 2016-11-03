using System;
using Aimp.DataAccess.Interfaces;
using Aimp.Domain;
using Aimp.Logic.Interfaces;
using Entities;
using System.Collections.Generic;
using System.Linq;

namespace Aimp.Logic.Services
{
    public class UserRightsService : IUserRightsService
    {
        public IQueryable<UserRight> GetUserRights(int id)
        {
            using (var context = IoC.Resolve<IDataContext>())
                return context.UserRights.All().Where(x => x.UserId == id);
        }
        public IQueryable<User> GetUsers()
        {
            using (var context = IoC.Resolve<IDataContext>())
                return context.Users.All();
        }
        public void SaveUser(IEnumerable<string> rightIds, User user)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                if (user.Id == 0)
                {
                    if(context.Users.All().Any(x=>x.Login == user.Login && x.Password == user.Password))
                        throw new ArgumentException($"User with login and password alredy exsits");
                }
                var oldRights = context.UserRights.All().Where(x => x.UserId == user.Id).ToList();
                if (user.Id != 0)
                {
                    var deleteIds = oldRights.Where(x => !rightIds.Contains(x.RightId)).Select(x => x.Id).ToArray();
                    context.UserRights.DeleteRange(deleteIds);
                }

                context.Users.AddOrUpdate(user);
                if (user.Id == 0)
                    context.SaveChanges();

                foreach (string iRight in rightIds)
                {
                    if (!oldRights.Any(x => x.RightId == iRight))
                    {
                        context.UserRights.AddOrUpdate(new UserRight()
                        {
                            UserId = user.Id,
                            RightId = iRight
                        });
                    }
                }
                context.SaveChanges();
            }
        }
        public void DeleteUser(User user)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                var rigths = context.UserRights.All().Where(x => x.UserId == user.Id).Select(x => x.Id).ToArray();
                context.UserRights.DeleteRange(rigths);
                context.Users.Delete(user);
                context.SaveChanges();
            }
        }
    }
}
