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
            logger.Log("---------------------------");
            logger.Log("start service.");
            try
            {
                ServicesInit(logger);

                using (WebServiceHost webServiceHost = new WebServiceHost(typeof(AimpWcfService)))
                {
                    if (webServiceHost.BaseAddresses.Count < 1)
                        throw new Exception("adress not found");

                    System.Console.Title = webServiceHost.BaseAddresses[0].AbsoluteUri;

                    logger.Log("web service host started.");
                    logger.Log("service ready!");
                    logger.Log("---------------------------");
                }
            }
            catch(Exception ex)
            {
                logger.Log(ex);
            }
            System.Console.ReadLine();
        }

        static void ServicesInit(ILogger logger)
        {
            IoC.Register<IDataContext, EfDataContext>(false);
            IoC.Resolve<IDataContext>();
            logger.Log("IDataContext ready.");

            IoC.Register<IUserRightsService, UserRightsService>();
            IoC.Resolve<IUserRightsService>();
            logger.Log("IUserRightsService ready.");

            IoC.Register<ITransactionService, TransactionService>();
            IoC.Resolve<ITransactionService>();
            logger.Log("ITransactionService ready.");

            IoC.Register<IReportOfClientService, ReportOfClientService>();
            IoC.Resolve<IReportOfClientService>();
            logger.Log("IReportOfClientService ready.");

            IoC.Register<IDocumentTemplateService, DocumentTemplateService>();
            IoC.Resolve<IDocumentTemplateService>();
            logger.Log("IDocumentTemplateService ready.");

            IoC.Register<ICreditTransactionService, CreditTransactionService>();
            IoC.Resolve<ICreditTransactionService>();
            logger.Log("ICreditTransactionService ready.");

            IoC.Register<ICommissionService, CommissionService>();
            IoC.Resolve<ICommissionService>();
            logger.Log("ICommissionService ready.");

            IoC.Register<ICashTransactionService, CashTransactionService>();
            IoC.Resolve<ICashTransactionService>();
            logger.Log("ICashTransactionService ready.");

            IoC.Register<ICardTrancportService, CardTrancportService>();
            IoC.Resolve<ICardTrancportService>();
            logger.Log("ICardTrancportService ready.");
        }
    }
}
