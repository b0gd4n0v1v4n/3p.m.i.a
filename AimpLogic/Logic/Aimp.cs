using AimpDataAccess.Context;
using AimpLogic.Logging;
using AimpLogic.UserRights;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AimpLogic.Logic
{
    public abstract class Aimp : IDisposable
    {
        protected readonly IAimpContext Context;
        public SystemUser User { get; }

        protected bool IsAdmin()
        {
            return User.RightIds.Any(x => x == Models.SecurityRigths.UserRightsCollection.Admin.Id);
        }
        protected void CheckViewRight()
        {
            if (User.RightIds.Count(x => x == Models.SecurityRigths.UserRightsCollection.View.Id || x == Models.SecurityRigths.UserRightsCollection.Admin.Id) == 0)
                throw new AccessDeniedException("У пользователя нет прав для выполнения операции");
        }
        protected void CheckAddRight()
        {
            if (User.RightIds.Count(x => x == Models.SecurityRigths.UserRightsCollection.Add.Id || x == Models.SecurityRigths.UserRightsCollection.Admin.Id) == 0)
                throw new AccessDeniedException("У пользователя нет прав для выполнения операции");
        }
        protected void CheckDeleteRight() 
        {
            if (User.RightIds.Count(x => x == Models.SecurityRigths.UserRightsCollection.Delete.Id || x == Models.SecurityRigths.UserRightsCollection.Admin.Id) == 0)
                throw new AccessDeniedException("У пользователя нет прав для выполнения операции");
        }
        protected void CheckQueryRight()
        {
            if (User.RightIds.Count(x => x==Models.SecurityRigths.UserRightsCollection.Add.Id || x == Models.SecurityRigths.UserRightsCollection.Delete.Id || x == Models.SecurityRigths.UserRightsCollection.Admin.Id) == 0)
                throw new AccessDeniedException("У пользователя нет прав для выполнения операции");
        }
        public Aimp(string login, string password)
        {
            Context = AimpContextResolve.Context;

            var userId = Context.Users
                                    .All()
                                    .Where(x => x.Login == login && x.Password == password)
                                    .Select(x => x.Id)
                                    .FirstOrDefault();

            if (userId > 0)
            {
                var rightIds = Context.UserRights
                    .All()
                    .Where(x => x.UserId == userId)
                    .Select(x => x.RightId)
                    .ToList();

                User = new SystemUser()
                {
                    Id = userId,
                    RightIds = rightIds
                };
            }
            else
                throw new AuthorizationException("Введён неверный логин или пароль");

        }
        public void Command(string query)
        {
            try
            {
                CheckQueryRight();
                Context.Command(query);
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);
                throw;
            }
        }
        public IEnumerable<T> Query<T>(string query) 
            where T : class
        {
            try
            {
                CheckViewRight();
                return Context.Query<T>(query);
            }
            catch(Exception ex)
            {
                Logger.Instance.Log(ex);
                throw;
            }
        }
        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
