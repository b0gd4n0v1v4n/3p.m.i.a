using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Aimp.Console.Wcf;

namespace Aimp.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebServiceHost webServiceHost = new WebServiceHost(typeof(AimpWcfService)))
            {
                System.Console.Title = webServiceHost.BaseAddresses[0].AbsoluteUri;

                System.Console.ReadLine();
            }
        }
    }
}
