using AimpLogic.Logging;
using AimpLogic.Logic;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AimpLogic.UserRights
{
    public class UserRightsService : Aimp, IUserRightsService
    {
        public UserRightsService(string login, string password)
            : base(login, password)
        { }
        
        public IQueryable<UserRight> GetUserRights(int id)
        {
            try
            {
                if (!IsAdmin())
                    throw new AccessDeniedException("У пользователя нет прав");

                return Context.UserRights.All().Where(x => x.UserId == id);
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);

                throw new Exception("Не удалось получить список прав пользователей, обратитесь к администратору");
            }
        }
        public IQueryable<User> GetUsers()
        {
            try
            {
                if (!IsAdmin())
                    throw new AccessDeniedException("У пользователя нет прав");

                return Context.Users.All();
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log("GetUsers", ex);

                throw new Exception("Не удалось получить список пользователей, обратитесь к администратору");
            }
        }
        public void SaveUser(IEnumerable<string> rightIds, User user)
        {
            try
            {
                if (!IsAdmin())
                    throw new AccessDeniedException("У пользователя нет прав");
                var oldRights = Context.UserRights.All().Where(x => x.UserId == user.Id).ToList();
                if (user.Id != 0)
                {
                    var deleteIds = oldRights.Where(x => !rightIds.Contains(x.RightId)).Select(x => x.Id).ToArray();
                    Context.UserRights.DeleteRange(deleteIds);
                }

                Context.Users.AddOrUpdate(user);
                if (user.Id == 0)
                    Context.SaveChanges();

                foreach (string iRight in rightIds)
                {
                    if (!oldRights.Any(x => x.RightId == iRight))
                    {
                        Context.UserRights.AddOrUpdate(new UserRight()
                        {
                            UserId = user.Id,
                            RightId = iRight
                        });
                    }
                }
                Context.SaveChanges();
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log("save user", ex);

                throw new Exception("Не удалось сохранить, обратитесь к администратору");
            }
        }
        public void DeleteUser(User user)
        {
            try
            {
                if (!IsAdmin())
                    throw new AccessDeniedException("У пользователя нет прав");

                var rigths = Context.UserRights.All().Where(x => x.UserId == user.Id).Select(x => x.Id).ToArray();
                Context.UserRights.DeleteRange(rigths);
                Context.Users.Delete(user);
                Context.SaveChanges();
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log("delete user", ex);

                throw new Exception("Не удалось получить список пользователей, обратитесь к администратору");
            }
        }
    }
}
