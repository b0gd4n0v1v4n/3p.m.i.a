using System;
using System.Linq;
using System.ServiceModel.Web;
using Aimp.Console.Wcf;
using Aimp.DataAccess.Ef;
using Aimp.DataAccess.Interfaces;
using Aimp.Domain;
using Aimp.Logic.Interfaces;
using Aimp.Logic.Services;

namespace Aimp.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            IoC.Register<ILogger, Logger>();
            var logger = IoC.Resolve<ILogger>();
            logger.LogEvent += System.Console.WriteLine;
            logger.Log("ILogger ready.");

            ServicesInit(logger);

            using (WebServiceHost webServiceHost = new WebServiceHost(typeof(AimpWcfService)))
            {
                if (webServiceHost.BaseAddresses.Count < 1)
                    throw new Exception("Adress not found");

                System.Console.Title = webServiceHost.BaseAddresses[0].AbsoluteUri;

                logger.Log("Web service host started.");
                logger.Log("AIMP READY!");

                System.Console.ReadLine();
            }
        }

        static void ServicesInit(ILogger logger)
        {
            IoC.Register<IDataContext, EfDataContext>(false);
            IoC.Resolve<IDataContext>().Users.Get(0);
            logger.Log("IDataContext ready.");

            IoC.Register<IUserRightsService, UserRightsService>();
            IoC.Resolve<IUserRightsService>().GetUsers(x=>x);
            logger.Log("IUserRightsService ready.");

            IoC.Register<ITransactionService, TransactionService>();
            IoC.Resolve<ITransactionService>().GetUserFile(0);
            logger.Log("ITransactionService ready.");

            IoC.Register<IReportOfClientService, ReportOfClientService>();
            IoC.Resolve<IReportOfClientService>().GetDocument(0);
            logger.Log("IReportOfClientService ready.");

            IoC.Register<IDocumentTemplateService, DocumentTemplateService>();
            IoC.Resolve<IDocumentTemplateService>().GetTemplate(0);
            logger.Log("IDocumentTemplateService ready.");

            IoC.Register<ICreditTransactionService, ICreditTransactionService>();
            IoC.Resolve<ICreditTransactionService>().GetDocument(0);
            logger.Log("ICreditTransactionService ready.");

            IoC.Register<ICommissionService, ICommissionService>();
            IoC.Resolve<ICommissionService>().GetDocument(0);
            logger.Log("ICommissionService ready.");

            IoC.Register<ICashTransactionService, ICashTransactionService>();
            IoC.Resolve<ICashTransactionService>().GetDocument(0);
            logger.Log("ICashTransactionService ready.");

            IoC.Register<ICardTrancportService, ICardTrancportService>();
            IoC.Resolve<ICardTrancportService>().GetDocument(0);
            logger.Log("ICardTrancportService ready.");
        }
    }
}
