using AimpLogic.Logging;
using AimpLogic.Logic;
using AimpLogic.UserRights;
using Models.Documents;
using Models.Entities;
using Models.PrintedDocument.Templates;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Models.PrintedDocument;
using System.Data.SqlTypes;
using AimpReports.Templates;
using AimpReports.Services.Word;
using Models;

namespace AimpLogic.CreditTransactions
{
    public class CreditTransactionService : Aimp, ICreditTransactionService
    {
        public CreditTransactionService(string login, string password)
            : base(login, password)
        { }
        public IQueryable<CreditTransaction> GetCreditTransactions()
        {
            try
            {
                if (IsAdmin())
                    return Context.CreditTransactions.All();
                else
                    return Context.CreditTransactions.All().Where(x => x.UserId == User.Id);
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);

                throw new Exception("Неудалось получить список сделок в кредит");
            }
        }
        public CreditTransactionDocument GetDocument(int id)
        {
            try
            {
                CheckViewRight();

                var creditTransaction = Context
                                .CreditTransactions
                                .Get(id, x => x.Seller.City,
                                        x => x.Seller.Region,
                                        x => x.Seller.Document,
                                        x => x.Seller.Photo,
                                        x => x.Buyer.City,
                                        x => x.Buyer.Region,
                                        x => x.Buyer.Document,
                                        x => x.Buyer.Photo,
                                        x => x.Owner.City,
                                        x => x.Owner.Region,
                                        x => x.Owner.Document,
                                        x => x.Owner.Photo,
                                        x => x.Trancport.Model,
                                        x => x.Trancport.Make,
                                        x => x.Trancport.Category,
                                        x => x.Trancport.CopyPts,
                                        x => x.Trancport.EngineType,
                                        x => x.Trancport.Type,
                                        x => x.DkpDocument,
                                        x => x.AgentDocument,
                                        x => x.Requisit,
                                        x => x.Creditor);

                return TinyMapper.Map<CreditTransactionDocument>(creditTransaction);
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);

                throw new Exception("Неудалось получить документ");
            }
        }
        public void SaveDocument(CreditTransactionDocument document)
        {
            try
            {

                CheckAddRight();

                var creditTransaction = TinyMapper.Map<CreditTransaction>(document);
                if (creditTransaction.Id == 0)
                    creditTransaction.UserId = User.Id;
                //else
                //{
                    
                //    if (document.DkpDocument != null)
                //    {
                //        CreditTransaction transaction = Context.CreditTransactions.Get(creditTransaction.Id,x=>x.DkpDocument);
                //        if (transaction.DkpDocumentId != document.DkpDocument.Id)
                //        {
                //            if (document.DkpDocument.Name != transaction.DkpDocument.Name)
                //            {
                //                Context.UserFiles.AddOrUpdate(document.DkpDocument);
                //                Context.SaveChanges();
                //                creditTransaction.DkpDocument = document.DkpDocument;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        CreditTransaction transaction = Context.CreditTransactions.Get(creditTransaction.Id);
                //        creditTransaction.DkpDocument = null;
                //        creditTransaction.DkpDocumentId = null;
                //        if(transaction.DkpDocumentId != null)
                //        {
                //            Context.UserFiles.DeleteRange(new[] { (int)transaction.DkpDocumentId });
                //        }
                //    }
                //    if (document.AgentDocument != null)
                //    {
                //        CreditTransaction transaction = Context.CreditTransactions.Get(creditTransaction.Id,x=>x.AgentDocument);
                //        if (transaction.AgentDocumentId != document.AgentDocument.Id)
                //        {
                //            if (document.AgentDocument.Name != transaction.AgentDocument.Name)
                //            {
                //                Context.UserFiles.AddOrUpdate(document.AgentDocument);
                //                Context.SaveChanges();
                //                creditTransaction.AgentDocument = document.AgentDocument;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        CreditTransaction transaction = Context.CreditTransactions.Get(creditTransaction.Id);
                //        creditTransaction.AgentDocument = null;
                //        creditTransaction.AgentDocumentId = null;
                //        if (transaction.AgentDocumentId != null)
                //        {
                //            Context.UserFiles.DeleteRange(new[] { (int)transaction.AgentDocumentId });
                //        }
                //    }
                //}
                Context.CreditTransactions.AddOrUpdate(creditTransaction);
                Context.SaveChanges();
                document.Id = creditTransaction.Id;
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);

                throw new Exception("Неудалось сохранить документ");
            }
        }
        public void DeleteDocument(CreditTransactionDocument document)
        {
            try
            {
                CheckDeleteRight();
                Context.CreditTransactions.Delete(document.Id);
                Context.SaveChanges();
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);

                throw new Exception("Неудалось удалить документ");
            }
        }
        public IQueryable<CreditTransaction> GetCashTransactions()
        {
            try
            {
                CheckViewRight();
                if (IsAdmin())
                    return Context.CreditTransactions.All();
                else
                    return Context.CreditTransactions.All().Where(x => x.UserId == User.Id);
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);

                throw new Exception("Неудалось получить список сделок за наличный расчет");
            }
        }
        public IQueryable<PrintedDocumentTemplate> GetPrintedDocumentTemplates()
        {
            try
            {
                CheckViewRight();
                string type = PrintedDocumentTemplateType.Кредит.ToString();
                string typeDkp = PrintedDocumentTemplateType.Дкп.ToString();
                string typeAkt = PrintedDocumentTemplateType.Акт.ToString();
                return Context.PrintedDocumentTemplates
                    .All()
                    .Where(x => x.Type == type || x.Type == typeDkp || x.Type == typeAkt);
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);

                throw new Exception("Не удалось получить список печатных форм, обратитесь к администратору");
            }
        }
        public IEnumerable<EntityName> GetPrintedList()
        {try { 
            string typeStr = PrintedDocumentTemplateType.Кредит.ToString();
            var result = Context.PrintedDocumentTemplates.All().Where(x => x.Type == typeStr).Select(x => new EntityName()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            result.Add(new EntityName()
            {
                Name = CreditTransactionPrintedDocumentTemplate.AKT_REPORT_NAME
            });
            result.Add(new EntityName()
            {
                Name = CreditTransactionPrintedDocumentTemplate.DKP_REPORT_NAME
            });
            return result;
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);

                throw new Exception("Не удалось получить список печатных форм, обратитесь к администратору");
            }
        }
        public WordPrintedDocument GetPrintedDocument(int idTransaction, string name)
        {
            try
            {
                CheckViewRight();
                var transaction = Context.CreditTransactions
                                .All(x => x.User,
                                    x => x.Buyer.City,
                                    x => x.Buyer.Region,
                                    x => x.Buyer.LegalPerson,
                                    x => x.Owner.City,
                                    x => x.Owner.Region,
                                    x => x.Owner.LegalPerson,
                                    x => x.Seller.City,
                                    x => x.Seller.Region,
                                    x => x.Seller.LegalPerson,
                                    x => x.Trancport.Make,
                                    x => x.Trancport.Model,
                                    x => x.Trancport.Type,
                                    x => x.Trancport.Category,
                                    x => x.Trancport.EngineType,
                                    x=>x.Creditor,
                                    x=>x.Requisit)
                                .FirstOrDefault(x => x.Id == idTransaction);
                if (transaction == null)
                    throw new SqlNullValueException("Документ не найден");
                PrintedDocumentTemplate template = null;
                if (name == CreditTransactionPrintedDocumentTemplate.AKT_REPORT_NAME || name == CreditTransactionPrintedDocumentTemplate.DKP_REPORT_NAME)
                {
                    var creditorName = transaction.Creditor.Name;
                    if (name == CreditTransactionPrintedDocumentTemplate.AKT_REPORT_NAME)
                    {
                        
                        string typeAkt = PrintedDocumentTemplateType.Акт.ToString();
                        template = Context.PrintedDocumentTemplates
                            .All()
                            .FirstOrDefault(x => x.Name == creditorName && x.Type == typeAkt);
                             
                    }
                    else
                    {
                        string typeDkp = PrintedDocumentTemplateType.Дкп.ToString();
                        template = Context.PrintedDocumentTemplates
                            .All()
                            .FirstOrDefault(x => x.Name == creditorName && x.Type == typeDkp);
                    }
                }
                else
                {
                    template = Context.PrintedDocumentTemplates
                                        .All()
                                        .FirstOrDefault(x => x.Name == name);
                }
            
                
                if (template == null)
                    throw new SqlNullValueException("Шаблон не найден");
                var resurl = new CreditTransactionPrintedDocumentTemplate(transaction, template.File);
                using (var printedService = new WordPrintedDocumentService())
                {
                    return (WordPrintedDocument)printedService.GetDocument(resurl);
                }
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);

                throw new Exception("Не удалось получить список печатных форм, обратитесь к администратору");
            }
        }
        public IQueryable<Creditor> GetCreditors()
        {
            try
            {
                CheckViewRight();
                return Context.Creditors.All();
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);

                throw new Exception("Не удалось получить список кредиторов, обратитесь к администратору");
            }
        }

        public IQueryable<Requisit> GetRequisits()
        {
            try
            {
                CheckViewRight();
                return Context.Requisits.All();
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);

                throw new Exception("Не удалось получить список реквизитов, обратитесь к администратору");
            }
        }
    }
}
