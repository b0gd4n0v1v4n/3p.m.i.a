using System;
using Aimp.Entities;
using Aimp.ServiceContracts.ClientReports;
using Aimp.Infrastructure.IoC;
using Aimp.DataContext;
using Aimp.Model.PrintedDocument;
using System.Linq;
using Aimp.PrintedDocument.DocumentBuilders.Excel;
using Aimp.UserRights;
using System.Collections.Generic;
using Aimp.Infrastructure;

namespace Aimp.Wcf.Services
{
    public class ClientReportServices : IClientReportContract
    {
        public void DeleteClientReport(IClientReport document)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    var clientBankReportIds = context.BankReportClients
                        .All()
                        .Where(x => x.ClientReportId == document.Id)
                        .Select(x => x.Id)
                        .ToArray();
                    context.BankReportClients.DeleteRange(clientBankReportIds);
                    context.ClientReports.Delete(document.Id);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public ClientReportDto GetClientReport(int id)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    var bankReportClients = context.BankReportClients
                                .All(x => x.ClientReport)
                                .Where(x => x.ClientReport.Id == id)
                                .ToList();

                    var clientStatuses = context.ClientStatuses.All().ToList();

                    var creditProgramms = context.CreditProgramms.All().ToList();

                    var banks = context.Banks.All().ToList();

                    var bankStatuses = context.BankStatuses.All().ToList();

                    var response = new ClientReportDto()
                    {
                        Banks = banks,
                        BankStatuses = bankStatuses,
                        ClientStatuses = clientStatuses,
                        CreditProgramms = creditProgramms,
                        BankReportClients = bankReportClients
                    };

                    return response;
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public ExcelPrintedDocument GetClientReportPrintedDocument(ClientReports reports)
        {
            try
            {
                using (var printService = new ExcelDocumentBuilder())
                {
                    return printService.GetReportOfClientList(reports);
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public ClientReports GetClientReports()
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    var result = new ClientReports();
                    result.UserLastNameForFilter = context.Users.Get(CurrentUserProvider.Account.Id).LastName;
                    result.ClientStatusesForFilter = context.ClientStatuses.All()
                        .Where(x => x.UsedFilter)
                        .Select(x => x.Name)
                        .ToList();
                    result.Banks = context.Banks.All().ToList();

                    IEnumerable<IBankReportClient> bankReportClients = null;
                    if (CurrentUserProvider.Account.IsAdmin())
                        bankReportClients = context.BankReportClients
                            .All(x => x.BankStatus, x => x.ClientReport.User, x => x.ClientReport.ClientStatus, x => x.ClientReport.CreditProgramm)
                            .ToList();
                    else
                        bankReportClients = context.BankReportClients
                            .All(x => x.BankStatus, x => x.ClientReport.User, x => x.ClientReport.ClientStatus, x => x.ClientReport.CreditProgramm)
                            .Where(x => x.ClientReport.User.Id == CurrentUserProvider.Account.Id)
                            .ToList();

                    result.Items = bankReportClients
                        .GroupBy(g => g.ClientReport)
                        .OrderByDescending(x => x.Key.Date)
                        .Select(x => new ClientReportListItem()
                        {
                            Id = x.Key.Id,
                            BankStatusesReportClient = (from b in result.Banks
                                                        join bs in x.Select(y => new { y.Bank.Id, y.BankStatus.MiddleName })
                                                        on b.Id equals bs.Id
                                                        into statusDefault
                                                        from bs in statusDefault.DefaultIfEmpty()
                                                        select bs?.MiddleName
                                                        ).ToArray(),
                            ClientStatusReportClient = x.Key.ClientStatus.Name,
                            DateReportClient = x.Key.Date.ToString(DataFormats.DateFormat),
                            FullNameReportClient = x.Key.FullName,
                            ManagerReportClient = x.Key.User.LastName,
                            PriceTrancportReportClient = x.Key.Price.ToString(),
                            ProgrammCreditReportClient = x.Key.CreditProgramm.Name,
                            SourceInfoReportClient = x.Key.Source,
                            TelefonReportClient = x.Key.Telefon,
                            TotalContributionReportClient = x.Key.TotalContribution?.ToString(),
                            TrancportNameReportClient = x.Key.Trancport
                        }).ToList();

                    return result;
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public ClientReportDto GetNewClientReport()
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    var clientStatuses = context.ClientStatuses.All().ToList();

                    var creditProgramms = context.CreditProgramms.All().ToList();

                    var banks = context.Banks.All().ToList();

                    var bankStatuses = context.BankStatuses.All().ToList();

                    return new ClientReportDto()
                    {
                        Banks = banks,
                        BankStatuses = bankStatuses,
                        ClientStatuses = clientStatuses,
                        CreditProgramms = creditProgramms
                    };
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public void SaveClientReport(IClientReport document)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {

                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }
    }
}
