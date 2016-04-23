using AimpLogic.Logging;
using AimpLogic.Logic;
using AimpLogic.UserRights;
using AimpReports.PrintedDocument.Templates;
using AimpReports.Services.Word;
using Models;
using Models.Documents;
using Models.Entities;
using Models.PrintedDocument;
using Models.PrintedDocument.Templates;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

namespace AimpLogic.CashTransactions
{
    public class CashTransactionService : Aimp, ICashTransactionService
    {
        public CashTransactionService(string login, string password)
            : base(login, password)
        { }
        public CashTransactionDocument GetDocument(int id)
        {
            try
            {
                CheckViewRight();

                var cashTransaction = Context
                .CashTransactions
        .Get(id, x => x.Seller.City,
                x => x.Seller.Region,
                x => x.Seller.LegalPerson,
                x => x.Buyer.City,
                x => x.Buyer.Region,
                x => x.Buyer.LegalPerson,
                x => x.Owner.City,
                x => x.Owner.Region,
                x => x.Owner.LegalPerson,
                x => x.Trancport.Model,
                x => x.Trancport.Make,
                x => x.Trancport.Category,
                x => x.Trancport.EngineType,
                x => x.Trancport.Type);

                return TinyMapper.Map<CashTransactionDocument>(cashTransaction);
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
        public void SaveDocument(CashTransactionDocument document)
        {
            try
            {
                CheckAddRight();

                var cashTransaction = TinyMapper.Map<CashTransaction>(document);
                if (cashTransaction.Id == 0)
                    cashTransaction.UserId = User.Id;
                Context.CashTransactions.AddOrUpdate(cashTransaction);
                Context.SaveChanges();
                document.Id = cashTransaction.Id;
                document.Number = cashTransaction.Number;
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
        public void DeleteDocument(CashTransactionDocument document)
        {
            try
            {
                CheckDeleteRight();
                Context.CashTransactions.Delete(document.Id);
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
        public IQueryable<CashTransaction> GetCashTransactions()
        {
            try
            {
                CheckViewRight();
                if (IsAdmin())
                    return Context.CashTransactions.All();
                else
                    return Context.CashTransactions.All().Where(x => x.UserId == User.Id);
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
                string type = PrintedDocumentTemplateType.Сделка.ToString();
                return Context.PrintedDocumentTemplates.All().Where(x=>x.Type == type);
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

                var transaction = Context.CashTransactions
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
                                    x => x.Trancport.EngineType)
                                .FirstOrDefault(x => x.Id == idTransaction);
                if (transaction == null)
                    throw new SqlNullValueException("Документ не найден");
                var typeId = PrintedDocumentTemplateType.Сделка.ToString();
                var fileTemplate =
                    Context.PrintedDocumentTemplates.All().FirstOrDefault(x => x.Name == name);
                if (fileTemplate == null)
                    throw new SqlNullValueException("Шаблон не найден");
                var template = new CashTransactionPrintedDocumentTemplate(transaction, fileTemplate.File);
                using (var printedService = new WordPrintedDocumentService())
                {
                    return (WordPrintedDocument)printedService.GetDocument(template);
                }
            }
            catch (SqlNullValueException)
            {
                throw;
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);

                throw new Exception("Не удалось получить печатную форму, обратитесь к администратору");
            }
        }
        public IEnumerable<EntityName> GetPrintedList()
        {
            string typeStr = PrintedDocumentTemplateType.Сделка.ToString();
            return Context.PrintedDocumentTemplates.All().Where(x => x.Type == typeStr).Select(x => new EntityName()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
    }
}

