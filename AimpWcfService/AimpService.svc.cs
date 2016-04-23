using AimpLogic;
using Models;
using Models.CashTransact;
using Models.Documents;
using Models.DTO;
using Models.ReportOfClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AimpWcfService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class AimpService : IAimpService
    {
        private string _password
        {
            get
            {
                //return WebOperationContext.Current.IncomingRequest.Headers.Get("password");
                return "admin";
            }
        }

        private string _login
        {
            get
            {
                return "admin";
                //return WebOperationContext.Current.IncomingRequest.Headers.Get("login");
            }
        }

        public ClientReports GetClientReports()
        {
            try
            {
                using (var aimp = new Aimp(_login, _password))
                {
                    var result = new ClientReports();

                    result.Banks = aimp.GetBanks();

                    result.Items = aimp.GetBankReportClients()
                        .GroupBy(g => g.ClientReport)
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
                            ClientStatusReportClient = x.Key.ClientStatus?.Name,
                            DateReportClient = x.Key.Date.ToString("dd.MM.yyyy"),
                            FullNameReportClient = x.Key.FullName,
                            ManagerReportClient = x.Key.User?.LastName,
                            PriceTrancportReportClient = x.Key.Price.ToString(),
                            ProgrammCreditReportClient = x.Key.CreditProgramm?.Name,
                            SourceInfoReportClient = x.Key.Source,
                            TelefonReportClient = x.Key.Telefon,
                            TotalContributionReportClient = x.Key.TotalContribution?.ToString(),
                            TrancportNameReportClient = x.Key.Trancport
                        });

                    return result;
                }
            }
            catch (Exception ex)
            {
                return new ClientReports(ex);
            }
        }

        public ClientReport GetNewClientReport()
        {
            try
            {
                using (var aimp = new Aimp(_login, _password))
                {
                    return new ClientReport(new ClientReportDocument(), aimp.GetClientStatuses(), aimp.GetCreditProgramms(), aimp.GetBanks(), aimp.GetBankStatuses());
                }
            }
            catch (Exception ex)
            {
                return new ClientReport(ex);
            }
        }

        public ClientReport GetClientReport(int id)
        {
            try
            {
                using (var aimp = new Aimp(_login, _password))
                {
                    var response = new ClientReport(aimp.GetDocument<ClientReportDocument>(id), aimp.GetClientStatuses(), aimp.GetCreditProgramms(), aimp.GetBanks(), aimp.GetBankStatuses());

                    return response;
                }
            }
            catch (Exception ex)
            {
                return new ClientReport(ex);
            }
        }

        public RegionsDto GetRegions()
        {
            RegionsDto regionsDto = new RegionsDto();

            try
            {
                //regionsDto.Regions = AimpDbContext.Context.Regions.ToList();
            }
            catch (Exception ex)
            {
                //regionsDto.Error = true;
                //regionsDto.MessageError = "Ошибка сервера: не удалось получить список";
                //ErrorLogger.Write(this, "GetRegions", ex);
            }

            return regionsDto;
        }

        public Response SaveClientReport(ClientReportDocument document)
        {
            try
            {
                using (var aimp = new Aimp(_login, _password))
                {
                    aimp.SaveDocument(document);
                }

                return new Response() { Message = "Данные успешно сохраненны" };
            }
            catch (Exception ex)
            {
                return new Response(ex);
            }
        }

        public CashTransactions GetCashTransactions()
        {
            try
            {
                using (var aimp = new Aimp(_login, _password))
                {
                    return new CashTransactions()
                    {
                        Items = aimp.GetCashTransactions().Select(x => new CashTransactionListItem()
                        {
                            Id = x.Id,
                            BuyerFullName = x.Buyer.LegalPerson != null ? x.Buyer.LegalPerson.Name : x.Buyer.LastName + " " + x.Buyer.FirstName + " " + x.Buyer.MiddleName,
                            SellerFullName = x.Seller.LegalPerson != null ? x.Seller.LegalPerson.Name : x.Seller.LastName + " " + x.Seller.FirstName + " " + x.Seller.MiddleName,
                            Date = x.Date.ToString("dd.MM.yyyy"),
                            DocumentBuyerId = x.Buyer.Document.Id,
                            DocumentSellerId = x.Seller.Document.Id,
                            Number = x.Number.ToString(),
                            NumberProxy = x.NumberProxy,
                            TrancportFullName = x.Trancport.Model.Name + "," + x.Trancport.Make.Name,
                            PtsId = x.Trancport.CopyPts.Id
                        })
                    };
                }
            }
            catch (Exception ex)
            {
                return new CashTransactions()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
    }
}