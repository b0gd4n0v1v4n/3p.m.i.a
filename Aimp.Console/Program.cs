using System;
using System.ServiceModel.Web;
using Aimp.Console.Wcf;

namespace Aimp.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebServiceHost webServiceHost = new WebServiceHost(typeof(AimpWcfService)))
            {
                if (webServiceHost.BaseAddresses.Count < 1)
                    throw new Exception("Adress not found");

                System.Console.Title = webServiceHost.BaseAddresses[0].AbsoluteUri;

                

                System.Console.ReadLine();
            }
        }
    }
}
