using System.Linq;
using System.Security.Authentication;
using System.ServiceModel.Web;
using Aimp.Domain;
using Aimp.Logic.Interfaces;
using Entities;
using System;

namespace Aimp.Console.Wcf
{
    public abstract class WcfServiceBase
    {
        protected User CurrentUser { get; private set; }
        protected ILogger Logger { get; private set; }

        public WcfServiceBase()
        {
            Logger = IoC.Resolve<ILogger>();

            try
            {
                var _service = IoC.Resolve<IUserRightsService>();

                var login = WebOperationContext.Current.IncomingRequest.Headers.Get("login");
                var password = WebOperationContext.Current.IncomingRequest.Headers.Get("password");
                
                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                    throw new AuthenticationException($"Login or password empty");

                CurrentUser = _service.GetUsers(x => x.Login == login && x.Password == password).FirstOrDefault();

                if (CurrentUser == null)
                    throw new AuthenticationException($"Неверный логин или пароль!");
            }
            catch(Exception ex)
            {
                Logger.Log(ex);
            }
        }

        protected void EventLog(string action)
        {
            Logger.Log($"[{CurrentUser.Login}]: {action}");
        }
    }
}
