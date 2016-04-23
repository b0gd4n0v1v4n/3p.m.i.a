using Models;
using Models.CashTransact;
using Models.Documents;
using Models.ReportOfClient;
using System;
using System.Collections.Generic;
using Models.TrancportInfo;
using Models.ContractorInfo;
using Models.CreditTransact;
using Models.Entities;
using Models.PrintedDocument;
using Models.PrintedDocument.Templates;
using Models.SecurityRigths;
using Models.UserFiles;
using ClientReport = Models.ReportOfClient.ClientReport;
using AimpConsole.Helpers;
using ServiceContract.Interfaces;
using System.ServiceModel.Web;
using Models.PrintDocumentTemplate;
using AimpReports.Services.Excel;
using Models.Commission;
using Models.CardTrancports;

namespace AimpConsole.Wcf
{
    class AimpWcfService: IAimpService
    {
        private void _WriteLineConsole(string message)
        {
            Console.WriteLine("");
            Console.WriteLine($"{DateTime.Now.ToLongTimeString()} | {AimpHelper.User.Login} | {message}");
        }
        private void _WriteLineError(string action,string message)
        {
            Console.WriteLine("");
            Console.WriteLine($"{DateTime.Now.ToLongTimeString()} | error: ");
            Console.WriteLine($"action : {action}");
            Console.WriteLine($"message : {message}");
        }
        public AimpWcfService() { 
            AimpHelper.User.Login = WebOperationContext.Current.IncomingRequest.Headers.Get("login");
            AimpHelper.User.Password = WebOperationContext.Current.IncomingRequest.Headers.Get("password");
        }
        #region client reports
        public ClientReports GetClientReports()
        {
            try
            {
                _WriteLineConsole("get client reports");

                using (var helper = new ClientReportHelper())
                {
                    return helper.GetClientReports();
                }
            }
            catch (Exception ex)
            {
                _WriteLineError("get client reports", ex.Message);
                return new ClientReports(ex);
            }
        }
        public ClientReport GetNewClientReport()
        {
            try
            {
                _WriteLineConsole("get new client report");

                using (var helper = new ClientReportHelper())
                {
                    return helper.GetNewClientReport();
                }
            }
            catch (Exception ex)
            {
                _WriteLineError("get new client report", ex.Message);
                return new ClientReport()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public ClientReport GetClientReport(int id)
        {
            try
            {
                _WriteLineConsole($"get client report id: {id}");

                using (var helper = new ClientReportHelper())
                {
                    return helper.GetClientReport(id);
                }
            }
            catch (Exception ex)
            {
                _WriteLineError("get client report", ex.Message);
                return new ClientReport()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public Response SaveClientReport(ClientReportDocument document)
        {
            try
            {
                _WriteLineConsole($"save client report identity: {document.Identity}");
                using (var helper = new ClientReportHelper())
                {
                    helper.SaveDocument(document);
                }

                return new Response() { Message = "Данные успешно сохраненны" };
            }
            catch (Exception ex)
            {
                _WriteLineError($"save client report identity: {document?.Identity}",ex.Message);
                return new Response(ex);
            }
        }
        public Response DeleteClientReport(ClientReportDocument document)
        {
            try
            {
                _WriteLineConsole($"delete client report identity: {document.Identity}");
                using (var helper = new ClientReportHelper())
                {
                    helper.DeleteDocument(document);
                }

                return new Response() { Message = "Документ удален" };
            }
            catch (Exception ex)
            {
                _WriteLineError($"delete client report identity: {document?.Identity}", ex.Message);
                return new Response(ex);
            }
        }
        #endregion
        #region cash tranasction
        public CashTransactionsDto GetCashTransactions()
        {
            try
            {
                _WriteLineConsole("get cash transactions");

                using (CashTransactionsHelper helper = new CashTransactionsHelper())
                    return helper.GetCashTransactions();
            }
            catch (Exception ex)
            {
                _WriteLineError("get cash transaction", ex.Message);
                return new CashTransactionsDto()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public CashTransactionDto GetCashTransaction(int id)
        {
            _WriteLineConsole($"get cash transaction id:{id}");

            try
            {
                using (CashTransactionsHelper helper = new CashTransactionsHelper())
                    return helper.GetCashTransaction(id);
            }
            catch (Exception ex)
            {
                _WriteLineError("get cash transaction", ex.Message);
                return new CashTransactionDto()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public Response DeleteCashTransaction(CashTransactionDocument document)
        {
            try
            {
                _WriteLineConsole($"delete cash transaction identity: {document.Identity}");
                using (var helper = new CashTransactionsHelper())
                {
                    helper.DeleteCashTransaction(document);
                }

                return new Response() { Message = "Документ удален" };
            }
            catch (Exception ex)
            {
                _WriteLineError($"delete cash transaction identity: {document?.Identity}", ex.Message);
                return new Response(ex);
            }
        }
        public SaveEntityResult SaveCashTransaction(CashTransactionDocument document)
        {
            try
            {
                _WriteLineConsole($"save cash transaction identity: {document.Identity}");
                using (var helper = new CashTransactionsHelper())
                    helper.SaveCashTransaction(document);

                return new SaveEntityResult() { Message = "Данные успешно сохраненны", Id = document.Id, Number = 3 };
            }
            catch (Exception ex)
            {
                _WriteLineError($"save cash transaction identity: {document?.Identity}", ex.Message);
                return new SaveEntityResult() {
                Error = true,
                Message = ex.Message};
            }
        }
        #endregion
        #region commission
        public CommissionsDto GetCommissions()
        {
            try
            {
                _WriteLineConsole("get commissions");

                using (var helper = new CommissionHelper())
                    return helper.GetCommissions();
            }
            catch (Exception ex)
            {
                _WriteLineError("get commissions", ex.Message);
                return new CommissionsDto()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public CommissionDto GetCommission(int id)
        {
            _WriteLineConsole($"get commission id:{id}");

            try
            {
                using (var helper = new CommissionHelper())
                    return helper.GetCommission(id);
            }
            catch (Exception ex)
            {
                _WriteLineError("get commission", ex.Message);
                return new CommissionDto()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public Response DeleteCommission(CommissionDocument document)
        {
            try
            {
                _WriteLineConsole($"delete commission identity: {document.Identity}");
                using (var helper = new CommissionHelper())
                {
                    helper.DeleteCommission(document);
                }

                return new Response() { Message = "Документ удален" };
            }
            catch (Exception ex)
            {
                _WriteLineError($"delete commission identity: {document?.Identity}", ex.Message);
                return new Response(ex);
            }
        }
        public SaveEntityResult SaveCommission(CommissionDocument document)
        {
            try
            {
                _WriteLineConsole($"save commission identity: {document.Identity}");
                using (var helper = new CommissionHelper())
                    helper.SaveCommision(document);

                return new SaveEntityResult() { Message = "Данные успешно сохраненны", Id = document.Id,Number = document.Number };
            }
            catch (Exception ex)
            {
                _WriteLineError($"save commission identity: {document?.Identity}", ex.Message);
                return new SaveEntityResult()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public SourcesTrancportDto GetSourceTrancport()
        {
            try
            {
                _WriteLineConsole("get source trancport");

                using (var helper = new CommissionHelper())
                    return helper.GetSourcesTrancport();
            }
            catch (Exception ex)
            {
                _WriteLineError("get source trancport", ex.Message);
                return new SourcesTrancportDto()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        #endregion
        #region credit tranasction
        public CreditTransactions GetCreditTransactions()
        {
            try
            {
                _WriteLineConsole("get Credit transactions");

                using (CreditTransactionHelper helper = new CreditTransactionHelper())
                    return helper.GetCreditTransactions();
            }
            catch (Exception ex)
            {
                _WriteLineError("get Credit transaction", ex.Message);
                return new CreditTransactions()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public CreditTransactionDto GetCreditTransaction(int id)
        {
            _WriteLineConsole($"get Credit transaction id:{id}");

            try
            {
                using (CreditTransactionHelper helper = new CreditTransactionHelper())
                    return helper.GetCreditTransaction(id);
            }
            catch (Exception ex)
            {
                _WriteLineError("get Credit transaction", ex.Message);
                return new CreditTransactionDto()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public Response DeleteCreditTransaction(CreditTransactionDocument document)
        {
            _WriteLineConsole($"delete Credit transaction identity: {document.Identity}");
            try
            {
                using (var helper = new CreditTransactionHelper())
                {
                    helper.DeleteCreditTransaction(document);
                }

                return new Response() { Message = "Документ удален" };
            }
            catch (Exception ex)
            {
                _WriteLineError($"delete Credit transaction identity: {document?.Identity}", ex.Message);
                return new Response(ex);
            }
        }
        public SaveEntityResult SaveCreditTransaction(CreditTransactionDocument document)
        {
            _WriteLineConsole($"save Credit transaction identity: {document.Identity}");
            try
            {
                using (var helper = new CreditTransactionHelper())
                    helper.SaveCreditTransaction(document);

                return new SaveEntityResult() { Message = "Данные успешно сохраненны",Id = document.Id,Number = document.Number };
            }
            catch (Exception ex)
            {
                _WriteLineError($"save credit transaction identity: {document?.Identity}", ex.Message);
                return new SaveEntityResult() {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        #endregion
        public TrancportDto GetTrancport(int id)
        {
            try
            {
                //using (var aimp = new Aimp(_login, _password))
                //{
                //    return new TrancportDto()
                //    {
                //        Trancport = aimp.GetTrancport(id)
                //    };
                //}
                return null;
            }
            catch (Exception ex)
            {
                return new TrancportDto()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public TrancportInfo GetTrancportInfo()
        {
            try
            {
                    _WriteLineConsole("get trancport info");
                    using (TransactionInfoHelper helper = new TransactionInfoHelper())
                        return helper.GetTrancportInfo();
            }
            catch (Exception ex)
            {
                _WriteLineError("get trancport info", ex.Message);
                return new TrancportInfo()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public ContractorInfo GetContractorInfo()
        {
            try
            {
                _WriteLineConsole("get contractor info");
                using (TransactionInfoHelper helper = new TransactionInfoHelper())
                    return helper.GetContractorInfo();
            }
            catch (Exception ex)
            {
                _WriteLineError("get contractor info",ex.Message);
                return new ContractorInfo()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }       
        public SearchContractorResult SearchContractors(TypeSearchContractor type, string text)
        {
            try
            {
                _WriteLineConsole($"Search contractor where {text}");
                using (var helper = new TransactionInfoHelper())
                    return new SearchContractorResult()
                    {
                        Contractors = helper.SearchContractors(type, text)
                    };
            }
            catch (Exception ex)
            {
                _WriteLineError($"Search contractor where {text}", ex.Message);
                return new SearchContractorResult()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public SearchTrancportResult SearchTrancports(TypeSearchTrancport type, string text)
        {
            try
            {
                _WriteLineConsole($"Search trancport where {text}");
                using (var helper = new TransactionInfoHelper())
                    return new SearchTrancportResult()
                    {
                        Trancports = helper.SearchTrancport(type, text)
                    };
            }
            catch (Exception ex)
            {
                _WriteLineError($"Search trancport where {text}", ex.Message);
                return new SearchTrancportResult()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public UserFileDto GetUserFile(int id)
        {
            try
            {
                _WriteLineConsole($"get user file id {id}");
                using (var helper = new TransactionInfoHelper())
                    return new UserFileDto()
                    {
                        UserFile = helper.GetUserFile(id)
                    };
            }
            catch (Exception ex)
            {
                _WriteLineError($"get user file id {id}", ex.Message);
                return new UserFileDto()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public SaveEntityResult SaveContractor(Contractor contractor)
        {
            try
            {
                _WriteLineConsole($"save contrctor {contractor.FirstName}");
                using (var helper = new TransactionInfoHelper())
                {
                    helper.SaveContractor(contractor);
                    return new SaveEntityResult()
                    {
                       Id = contractor.Id
                    };
                }
            }
            catch (Exception ex)
            {
                _WriteLineError($"save contrctor {contractor?.FirstName}", ex.Message);
                return new SaveEntityResult()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public SaveEntityResult SaveTrancport(Trancport trancport)
        {
            try
            {
                _WriteLineConsole($"save trancport {trancport.Model?.Name}");
                using (var helper = new TransactionInfoHelper())
                {
                    helper.SaveTrancport(trancport);
                    return new SaveEntityResult()
                    {
                        Id = trancport.Id
                    };
                }
            }
            catch (Exception ex)
            {
                _WriteLineError($"save trancport {trancport?.Model?.Name}", ex.Message);
                return new SaveEntityResult()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public UserRightsDto GetUserRights(int id)
        {
            try
            {
                _WriteLineConsole($"get user rights id {id}");
                using (var helper = new UserRightsHelper())
                {
                    return new UserRightsDto()
                    {
                        UserRights = helper.GetUserRights(id)
                    };
                }
            }
            catch (Exception ex)
            {
                _WriteLineError($"get user rights {id}", ex.Message);
                return new UserRightsDto()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public UsersDto GetUsers()
        {
            try
            {
                _WriteLineConsole("get users");
                using (var helper = new UserRightsHelper())
                {
                    return new UsersDto()
                    {
                        Users = helper.GetUsers()
                    };
                }
            }
            catch (Exception ex)
            {
                _WriteLineError("get users", ex.Message);
                return new UsersDto()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public SaveEntityResult SaveUser(User user, IEnumerable<string> rightIds)
        {
            try
            {
                _WriteLineConsole("save user");
                using (var helper = new UserRightsHelper())
                {
                    helper.SaveUser(user,rightIds);
                    return new SaveEntityResult()
                    {
                        Id = user.Id
                    };
                }
            }
            catch (Exception ex)
            {
                _WriteLineError("save user", ex.Message);
                return new SaveEntityResult()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public Response DeleteUser(User user)
        {
            try
            {
                _WriteLineConsole("delete user");
                using (var helper = new UserRightsHelper())
                {
                    helper.DeleteUser(user);
                    return new Response()
                    {
                        Message = "Пользователь удалён"
                    };
                }
            }
            catch (Exception ex)
            {
                _WriteLineError("delete user", ex.Message);
                return new Response()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public WordPrintedDocumentDto GetPrintedDocument(DocumentType type, string name,int id)
        {
            try
            {
                _WriteLineConsole("get printed document");
                switch (type)
                {
                    case DocumentType.CashTransaction:
                        {
                            using(CashTransactionsHelper helper = new CashTransactionsHelper())
                            {
                                return helper.GetPrintedDocument(id,name);
                            }
                        }
                    case DocumentType.CreditTransaction:
                        {
                            using(var helper = new CreditTransactionHelper())
                            {
                                return helper.GetPrintedDocument(id, name);
                            }
                        }
                    default:
                        throw new NotImplementedException();
                }
            }
            catch (Exception ex)
            {
                _WriteLineError("get printed document", ex.Message);
                return new WordPrintedDocumentDto()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public PrintedDocumentsListDto GetPrintedList(DocumentType type)
        {
            try
            {
                _WriteLineConsole("get printed list");
                switch (type)
                {
                    case DocumentType.CashTransaction:
                        {
                            using (CashTransactionsHelper helper = new CashTransactionsHelper())
                            {
                                return helper.GetPrintedList();
                            }
                        }
                    case DocumentType.CreditTransaction:
                        {
                            using (var helper = new CreditTransactionHelper())
                            {
                                return helper.GetPrintedList();
                            }
                        }
                    case DocumentType.Commission:
                        {
                            using(var helper = new CommissionHelper())
                            {
                                return helper.GetPrintedList();
                            }
                        }
                    default:
                        throw new NotImplementedException();
                }
            }
            catch (Exception ex)
            {
                _WriteLineError("get printed list", ex.Message);
                return new PrintedDocumentsListDto()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public AimpUserDto Auth()
        {
            try
            {
                _WriteLineConsole($"auth user login:{AimpHelper.User.Login} password:{AimpHelper.User.Password}");
                using(var service = new UserRightsHelper())
                {
                    var user = service.GetUser();
                    return new AimpUserDto()
                    {
                        Id = user.Id,
                        UserRights = user.RightIds
                    };
                }
            }
            catch (Exception ex)
            {
                _WriteLineError($"auth user login:{AimpHelper.User.Login} password:{AimpHelper.User.Password}", ex.Message);
                return new AimpUserDto()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public PrintedDocumentTemplateDto GetPrintedDocTemplate(int id)
        {
            try
            {
                _WriteLineConsole($"get printed doc template {id}");

                using (var helper = new PrintedDocumentTemplateHelper())
                {
                    return new PrintedDocumentTemplateDto()
                    {
                        Template = helper.GetTemplate(id)
                    };
                }

            }
            catch (Exception ex)
            {
                _WriteLineError($"get printed doc template {id}", ex.Message);
                return new PrintedDocumentTemplateDto()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public PrintedDocumentTemplatesListDto GetPrintedDocTemplatesList()
        {
            try
            {
                _WriteLineConsole($"get printed doc template list");

                using (var helper = new PrintedDocumentTemplateHelper())
                {
                    return new PrintedDocumentTemplatesListDto()
                    {
                        Items = helper.GetList()
                    };
                }

            }
            catch (Exception ex)
            {
                _WriteLineError($"get printed doc template list", ex.Message);
                return new PrintedDocumentTemplatesListDto()
                {
                    Error = true,
                    Message = ex.Message
                };
            }

        }
        public SaveEntityResult SavePrintedDocTemplate(PrintedDocumentTemplate template)
        {
            try
            {
                _WriteLineConsole($"save printed doc template id:{template.Id}");

                using (var helper = new PrintedDocumentTemplateHelper())
                {
                    return new SaveEntityResult()
                    {
                        Id = helper.SaveTemplate(template)
                    };
                }

            }
            catch (Exception ex)
            {
                _WriteLineError($"save printed doc template id:{template?.Id}", ex.Message);
                return new SaveEntityResult()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public Response DeletePrintedDocTemplate(PrintedDocumentTemplate template)
        {
            try
            {
                _WriteLineConsole($"delete print template id:{template.Id}");

                using (var helper = new PrintedDocumentTemplateHelper())
                {
                    helper.DeleteTemplate(template);
                    return new Response()
                    {
                        Message = "Печатная форма удалена"
                    };
                }

            }
            catch (Exception ex)
            {
                _WriteLineError($"delete printed doc template id:{template?.Id}", ex.Message);
                return new Response()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public DictionaryDto GetDictionary(string tableName)
        {
            try
            {
                _WriteLineConsole($"get dictionary:{tableName}");

                using (var helper = new PrintedDocumentTemplateHelper())
                {
                    return new DictionaryDto()
                    {
                        Items = helper.GetDictionary(tableName)
                    };
                }

            }
            catch (Exception ex)
            {
                _WriteLineError($"get dictionary:{tableName}", ex.Message);
                return new DictionaryDto()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public Response SaveRowDictionary(string tableName, string value,int id)
        {
            try
            {
                _WriteLineConsole($"save dictionary:{tableName}");

                using (var helper = new PrintedDocumentTemplateHelper())
                {
                    helper.SaveRowDictionary(tableName, value,id);
                }
                return new Response()
                {
                    Message = "Сохранено"
                };
            }
            catch (Exception ex)
            {
                _WriteLineError($"save dictionary:{tableName}", ex.Message);
                return new Response()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public Response DeleteRowDictionary(string tableName, int id)
        {
            try
            {
                _WriteLineConsole($"delte dictionary:{tableName} id:{id}");

                using (var helper = new PrintedDocumentTemplateHelper())
                {
                    helper.DeleteRowDictionary(tableName, id);
                }
                return new Response()
                {
                    Message = "Удалено"
                };
            }
            catch (Exception ex)
            {
                _WriteLineError($"delete dictionary:{tableName} id:{id}", ex.Message);
                return new Response()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public Response SaveRowValuesDictionary(string tableName, IDictionary<string, string> columnValues, int id)
        {
            try
            {
                _WriteLineConsole($"update values dictionary:{tableName}");

                using (var helper = new PrintedDocumentTemplateHelper())
                {
                    helper.SaveRowDictionary(tableName, columnValues, id);
                }
                return new Response()
                {
                    Message = " Сохранено"
                };
            }
            catch (Exception ex)
            {
                _WriteLineError($"update values dictionary:{tableName}", ex.Message);
                return new Response()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public ExcelPrintedDocumentDto GetClientReportPrintedDocument(ClientReports reports)
        {
            try
            {
                _WriteLineConsole("get client report printed document");
                
                using(var printedService = new ClientReportHelper())
                {
                    return new ExcelPrintedDocumentDto()
                    {
                        Document = printedService.GetPrintedReportList(reports)
                    };
                }
            }
            catch (Exception ex)
            {
                _WriteLineError("get client report printed document", ex.Message);
                return new ExcelPrintedDocumentDto()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public CreditTransactionInfoDto GetCreditTransactionInfo()
        {
            try
            {
                _WriteLineConsole("get credit transact info");

                using (var service = new CreditTransactionHelper())
                {
                    return service.GetCreditTransactionInfo();
                }
            }
            catch (Exception ex)
            {
                _WriteLineError("get client report printed document", ex.Message);
                return new CreditTransactionInfoDto()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        #region card trancport
        public CardTrancportsDto GetCardsTrancport()
        {
            try
            {
                _WriteLineConsole("get cards trancport");

                using (var helper = new CardsTrancportHelper())
                    return helper.GetCards();
            }
            catch (Exception ex)
            {
                _WriteLineError("get cards trancport", ex.Message);
                return new CardTrancportsDto()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public CardTrancportDto GetCardTrancport(int id)
        {
            _WriteLineConsole($"get card trancport id:{id}");

            try
            {
                using (var helper = new CardsTrancportHelper())
                    return helper.GetCard(id);
            }
            catch (Exception ex)
            {
                _WriteLineError("get card trancport", ex.Message);
                return new CardTrancportDto()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public SaveEntityResult AddCardTrancport(int idCommission)
        {
            try
            {
                _WriteLineConsole($"add card trancport id commission: {idCommission}");
                using (var helper = new CardsTrancportHelper())
                {
                    return new SaveEntityResult() { Message = "Данные успешно сохраненны", Id = helper.AddCard(idCommission)
                };
                }
            }
            catch (Exception ex)
            {
                _WriteLineError($"add card trancport commission id: {idCommission}", ex.Message);
                return new SaveEntityResult()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public SaveEntityResult SaveCardTrancport(CardTrancportDocument document)
        {
            try
            {
                _WriteLineConsole($"save card trancport: {document.Identity}");
                using (var helper = new CardsTrancportHelper())
                    helper.SaveCard(document);

                return new SaveEntityResult() { Message = "Данные успешно сохраненны", Id = document.CardTrancport.Id };
            }
            catch (Exception ex)
            {
                _WriteLineError($"save card trancport: {document?.Identity}", ex.Message);
                return new SaveEntityResult()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        public Response DeleteCardTrancport(int idCommission)
        {
            try
            {
                _WriteLineConsole($"delete card trancport idCommission: {idCommission}");
                using (var helper = new CardsTrancportHelper())
                {
                    helper.DeleteCard(idCommission);
                }

                return new Response() { Message = "Документ удален" };
            }
            catch (Exception ex)
            {
                _WriteLineError($"delete card trancport idCommission: {idCommission}", ex.Message);
                return new Response(ex);
            }
        }

        public StatusesCardTrancportDto GetStatusesCardTrancport()
        {
            try
            {
                _WriteLineConsole("get statuses card trancport");

                using (var helper = new CardsTrancportHelper())
                    return helper.GetStatusesCard();
            }
            catch (Exception ex)
            {
                _WriteLineError("get statuses card trancport", ex.Message);
                return new StatusesCardTrancportDto()
                {
                    Error = true,
                    Message = ex.Message
                };
            }
        }
        #endregion
    }
}