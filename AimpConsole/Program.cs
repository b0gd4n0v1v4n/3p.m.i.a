using AimpConsole.Wcf;
using AimpLogic.Logging;
using AimpLogic.UserRights;
using System;
using System.ServiceModel.Web;

namespace AimpConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebServiceHost webServiceHost = new WebServiceHost(typeof(AimpWcfService)))
            {
                webServiceHost.Open();
                try
                {
                    using(UserRightsService aimp = new UserRightsService("",""))
                    {

                    }
                    Console.WriteLine("Сервис запущен...");
                }
                catch(AuthorizationException)
                {
                    Console.WriteLine("Сервис запущен...");
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Не удалось запустить сервис");
                    Console.WriteLine(ex.Message);
                }
                Console.ReadLine();
            }
        }
    }
}
