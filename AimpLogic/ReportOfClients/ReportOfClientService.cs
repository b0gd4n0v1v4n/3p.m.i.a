using AimpLogic.Logging;
using AimpLogic.Logic;
using AimpLogic.UserRights;
using AimpReports.Services.Excel;
using Models.Documents;
using Models.Entities;
using Models.PrintedDocument;
using Models.ReportOfClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AimpLogic.ReportOfClients
{
    public class ReportOfClientService : Aimp, IReportOfClientService
    {
        public ReportOfClientService(string login,string password) 
            :base(login,password)
        {

        }
        public ClientReportDocument GetDocument(int id)
        {
            try
            {
                CheckViewRight();
                var bankReportClient = Context.BankReportClients
                                .All(x => x.ClientReport)
                                .Where(x => x.ClientReport.Id == id)
                                .ToList();

                return new ClientReportDocument()
                {
                    BankReportClients = bankReportClient
                };
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);
                throw new Exception("Не удалось получить документ, обратитесь к администратору");
            }
        }
        public IPrintedDocument PrintReport(ClientReports report)
        {
            try
            {
                CheckViewRight();
                
                using(var printService = new ExcelPrintedDocumentService())
                {
                    return printService.GetReportOfClientList(report);
                }
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);
                throw new Exception("Не удалось сформировать отчет, обратитесь к администратору");
            }
        }
        public void SaveDocument(ClientReportDocument document)
        {
            try
            {
                CheckAddRight();
                var firstClientReposrt = document.BankReportClients.FirstOrDefault().ClientReport;
                if (firstClientReposrt.Id == 0)
                {

                    firstClientReposrt.UserId = User.Id;
                    Context.ClientReports.AddOrUpdate(firstClientReposrt);
                    foreach (var iBankReportClient in document.BankReportClients)
                    {
                        iBankReportClient.ClientReport = firstClientReposrt;

                        Context.BankReportClients.AddOrUpdate(iBankReportClient);
                    }
                }
                else
                {
                    var oldBankReportClients = Context.BankReportClients
                                               .All()
                                               .Where(x => x.ClientReport.Id == firstClientReposrt.Id)
                                               .ToList();
                    foreach (BankReportClient iClientBankReport in oldBankReportClients)
                    {
                        var bank =
                            document.BankReportClients.FirstOrDefault(
                                x => x.Id == iClientBankReport.Id);
                        if (bank == null)
                            Context.BankReportClients.Delete(iClientBankReport);
                        else
                            Context.BankReportClients.AddOrUpdate(bank);
                    }
                    foreach(var iNewBank in document.BankReportClients.Where(x=>x.Id == 0))
                    {
                        Context.BankReportClients.AddOrUpdate(iNewBank);
                    }
                    Context.ClientReports.AddOrUpdate(firstClientReposrt);
                }
                Context.SaveChanges();
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);
                throw new Exception("Не удалось сохранить документ, обратитесь к администратору");
            }
        }
        public void DeleteDocument(ClientReportDocument document)
        {
            try
            {
                CheckDeleteRight();
                var clientReportDocument = document as ClientReportDocument;
                var firstClientReposrt = clientReportDocument.BankReportClients.FirstOrDefault().ClientReport;
                var clientBankReportIds = Context.BankReportClients
                    .All()
                    .Where(x => x.ClientReportId == firstClientReposrt.Id)
                    .Select(x => x.Id)
                    .ToArray();
                Context.BankReportClients.DeleteRange(clientBankReportIds);
                Context.ClientReports.Delete(firstClientReposrt.Id);
                Context.SaveChanges();
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);
                throw new Exception("Не удалось удалить документ, обратитесь к администратору");
            }
        }
        public IEnumerable<BankStatus> GetBankStatuses()
        {
            try
            {
                return Context.BankStatuses.All().ToList();
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);

                throw new Exception("Неудалось получить список статусов банков");
            }
        }
        public IEnumerable<Bank> GetBanks()
        {
            try
            {
                return Context.Banks.All().ToList();
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);

                throw new Exception("Неудалось получить список банков");
            }
        }
        public IEnumerable<CreditProgramm> GetCreditProgramms()
        {
            try
            {
                return Context.CreditProgramms.All().ToList();
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);

                throw new Exception("Неудалось получить список программ кредитования");
            }
        }
        public IEnumerable<ClientStatus> GetClientStatuses()
        {
            try
            {
                return Context.ClientStatuses.All().ToList();
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);

                throw new Exception("Неудалось получить список статусов клиентов");
            }
        }
        public IQueryable<BankReportClient> GetBankReportClients()
        {
            try
            {
                if (IsAdmin())
                    return Context.BankReportClients
                        .All(x => x.BankStatus, x => x.ClientReport.User, x => x.ClientReport.ClientStatus, x => x.ClientReport.CreditProgramm);
                else
                    return Context.BankReportClients
                        .All(x => x.BankStatus, x => x.ClientReport.User, x => x.ClientReport.ClientStatus, x => x.ClientReport.CreditProgramm)
                        .Where(x => x.ClientReport.User.Id == User.Id);
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);

                throw new Exception("Неудалось получить список клиентских отчетов");
            }
        }
        public User GetUser()
        {
            return Context.Users.Get(User.Id);
        }
    }
}
