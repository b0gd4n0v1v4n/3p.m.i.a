using AimpLogic.Logging;
using AimpLogic.Logic;
using AimpLogic.UserRights;
using AimpReports.PrintedDocument.Templates;
using AimpReports.Services.Word;
using AimpReports.Templates;
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
using System.Text;
using System.Threading.Tasks;

namespace AimpLogic.Commission
{
    public class CommissionService : Aimp, ICommissionService
    {
        public CommissionService(string login, string password)
            : base(login, password)
        { }
        public CommissionDocument GetDocument(int id)
        {
            try
            {
                CheckViewRight();

                var commission = Context
                .CommissionTransactions
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
                x => x.Trancport.Type,
                x => x.SourceTrancport);

                return TinyMapper.Map<CommissionDocument>(commission);
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
        public void SaveDocument(CommissionDocument document)
        {
            try
            {
                CheckAddRight();

                var commission = TinyMapper.Map<CommissionTransaction>(document);
                if (commission.Id == 0)
                    commission.UserId = User.Id;
                Context.CommissionTransactions.AddOrUpdate(commission);
                Context.SaveChanges();
                document.Id = commission.Id;
                document.Number = commission.Number;
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
        public void DeleteDocument(CommissionDocument document)
        {
            try
            {
                CheckDeleteRight();
                Context.CommissionTransactions.Delete(document.Id);
                var dleteCardIds = Context.CardsTrancport
                    .All()
                    .Where(x => x.CommissionTransaction.Id == document.Id)
                    .Select(x => x.Id).ToArray();
                Context.CardsTrancport
                    .DeleteRange(dleteCardIds);
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
        public IQueryable<CommissionTransaction> GetCommissions()
        {
            try
            {
                CheckViewRight();
                if (IsAdmin())
                    return Context.CommissionTransactions.All();
                else
                    return Context.CommissionTransactions.All().Where(x => x.UserId == User.Id);
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);

                throw new Exception("Неудалось получить список");
            }
        }
        public IQueryable<PrintedDocumentTemplate> GetPrintedDocumentTemplates()
        {
            try
            {
                CheckViewRight();
                string type = PrintedDocumentTemplateType.Комиссия.ToString();
                return Context.PrintedDocumentTemplates.All().Where(x => x.Type == type);
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
        public WordPrintedDocument GetPrintedDocument(int idCommission, string name)
        {
            try
            {
                CheckViewRight();

                var transaction = Context.CommissionTransactions
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
                                .FirstOrDefault(x => x.Id == idCommission);
                if (transaction == null)
                    throw new SqlNullValueException("Документ не найден");
                var typeId = PrintedDocumentTemplateType.Сделка.ToString();
                var fileTemplate =
                    Context.PrintedDocumentTemplates.All().FirstOrDefault(x => x.Name == name);
                if (fileTemplate == null)
                    throw new SqlNullValueException("Шаблон не найден");
                var template = new CommissionTransactionPrintedDocumentTemplate(transaction, fileTemplate.File);
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
            string typeStr = PrintedDocumentTemplateType.Комиссия.ToString();
            return Context.PrintedDocumentTemplates.All().Where(x => x.Type == typeStr).Select(x => new EntityName()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public IQueryable<SourceTrancport> GetSourcesTrancport()
        {
            try
            {
                CheckViewRight();
                return Context.SourcesTrancport.All();
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);

                throw new Exception("Не удалось получить список, обратитесь к администратору");
            }
        }
    }
}
