using Aimp.DataContext;
using Aimp.Entities;
using Aimp.Infrastructure.IoC;
using Aimp.Infrastructure.Users.Exeptions;
using Aimp.UserRights;
using Aimp.UserRights.Services;
using System.Collections.Generic;
using System.Linq;

namespace Aimp.Logic.UserRights
{
    public class UserRightsService :  IUserRightsService
    {
        public IEnumerable<IUserRight> GetUserRights(int id)
        {
            using (var context = IoC.Resolve<IAimpContext>())
                return context.UserRights.All().Where(x => x.UserId == id);
        }
        public IEnumerable<IUser> GetUsers()
        {
            using (var context = IoC.Resolve<IAimpContext>())
                return context.Users.All();
        }
        public void SaveUser(IEnumerable<string> rightIds, IUser user)
        {
            using (var context = IoC.Resolve<IAimpContext>())
            {
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
                        var userRight = context.UserRights.Create();
                        userRight.UserId = user.Id;
                        userRight.RightId = iRight;
                        context.UserRights.AddOrUpdate(userRight);
                    }
                }
                context.SaveChanges();
            }
        }
        public void DeleteUser(IUser user)
        {
            using (var context = IoC.Resolve<IAimpContext>())
            {
                var rigths = context.UserRights.All().Where(x => x.UserId == user.Id).Select(x => x.Id).ToArray();
                context.UserRights.DeleteRange(rigths);
                context.Users.Delete(user);
                context.SaveChanges();
            }
        }

        public IAccount Auth(string login, string password)
        {
            using (var context = IoC.Resolve<IAimpContext>())
            {
                var userId = context.Users
                                    .All()
                                    .Where(x => x.Login == login && x.Password == password)
                                    .Select(x => x.Id)
                                    .FirstOrDefault();

                if (userId > 0)
                {
                    var rightIds = context.UserRights
                        .All()
                        .Where(x => x.UserId == userId)
                        .Select(x => x.RightId)
                        .ToList();

                    return new Accaunt()
                    {
                        Id = userId,
                        RightIds = rightIds
                    };
                }
                else
                    throw new AuthorizationException("Введён неверный логин или пароль");
            }
        }
    }
}
