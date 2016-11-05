using Aimp.DataAccess.Interfaces;
using Aimp.Domain;
using Aimp.Logic.Extensions;
using Aimp.Logic.Interfaces;
using Aimp.Model.Documents;
using Aimp.Model.PrintedDocument;
using Aimp.Model.ReportOfClient;
using Aimp.Reports.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aimp.Logic.Services
{
    public class ReportOfClientService : IReportOfClientService
    {
        public ClientReportDocument GetDocument(int id)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                var bankReportClient = context.BankReportClients
                                .All(x => x.ClientReport)
                                .Where(x => x.ClientReport.Id == id)
                                .ToList();

                return new ClientReportDocument()
                {
                    BankReportClients = bankReportClient
                };
            }
        }
        public IPrintedDocument PrintReport(IEnumerable<Bank> banks, IEnumerable<ClientReportListItem> reports)
        {
            using (var printService = IoC.Resolve<IExcelPrintedService>())
            {
                return printService.GetReportOfClientList(banks,reports);
            }
        }
        public void SaveDocument(ClientReportDocument document)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                var firstClientReposrt = document.BankReportClients.FirstOrDefault().ClientReport;
                if (firstClientReposrt.Id == 0)
                {
                    if (document.UserId == 0)
                        throw new ArgumentException("UserId");

                    firstClientReposrt.UserId = document.UserId;

                    context.ClientReports.AddOrUpdate(firstClientReposrt);
                    foreach (var iBankReportClient in document.BankReportClients)
                    {
                        iBankReportClient.ClientReport = firstClientReposrt;

                        context.BankReportClients.AddOrUpdate(iBankReportClient);
                    }
                    context.SaveChanges();
                    document.Id = firstClientReposrt.Id;
                }
                else
                {
                    var oldBankReportClients = context.BankReportClients
                                               .All(x=>x.ClientReport)
                                               .Where(x => x.ClientReport.Id == firstClientReposrt.Id)
                                               .ToList();
                    foreach (BankReportClient iClientBankReport in oldBankReportClients)
                    {
                        var bank =
                            document.BankReportClients.FirstOrDefault(
                                x => x.Id == iClientBankReport.Id);
                        if (bank == null)
                            context.BankReportClients.Delete(iClientBankReport);
                        else
                            context.BankReportClients.AddOrUpdate(bank);
                    }
                    foreach (var iNewBank in document.BankReportClients.Where(x => x.Id == 0))
                    {
                        context.BankReportClients.AddOrUpdate(iNewBank);
                    }
                    firstClientReposrt.UserId = oldBankReportClients.First().ClientReport.UserId;
                    context.ClientReports.AddOrUpdate(firstClientReposrt);
                    context.SaveChanges();
                }
            }
        }
        public void DeleteDocument(ClientReportDocument document)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                var clientReportDocument = document as ClientReportDocument;
                var firstClientReposrt = clientReportDocument.BankReportClients.FirstOrDefault().ClientReport;
                var clientBankReportIds = context.BankReportClients
                    .All()
                    .Where(x => x.ClientReportId == firstClientReposrt.Id)
                    .Select(x => x.Id)
                    .ToArray();
                context.BankReportClients.DeleteRange(clientBankReportIds);
                context.ClientReports.Delete(firstClientReposrt.Id);
                context.SaveChanges();
            }
        }
        public IEnumerable<BankStatus> GetBankStatuses()
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                return context.BankStatuses.All().ToList();
            }
        }
        public IEnumerable<Bank> GetBanks()
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                return context.Banks.All().ToList();
            }
        }
        public IEnumerable<CreditProgramm> GetCreditProgramms()
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                return context.CreditProgramms.All().ToList();
            }
        }
        public IEnumerable<ClientStatus> GetClientStatuses()
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                return context.ClientStatuses.All().ToList();
            }
        }
        public IEnumerable<BankReportClient> GetBankReportClients(User user)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                if (user.IsAdmin())
                    return context.BankReportClients
                            .All(x => x.BankStatus, x => x.ClientReport.User, x => x.ClientReport.ClientStatus, x => x.ClientReport.CreditProgramm)
                            .ToList();
                
                    return context.BankReportClients
                        .All(x => x.BankStatus, x => x.ClientReport.User, x => x.ClientReport.ClientStatus, x => x.ClientReport.CreditProgramm)
                        .Where(x => x.ClientReport.User.Id == user.Id)
                        .ToList();
            }
        }
    }
}
