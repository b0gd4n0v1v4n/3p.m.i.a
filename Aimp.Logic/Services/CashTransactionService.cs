using Aimp.DataAccess.Interfaces;
using Aimp.Domain;
using Aimp.Logic.Extensions;
using Aimp.Logic.Interfaces;
using Aimp.Model;
using Aimp.Model.Documents;
using Aimp.Model.PrintedDocument;
using Aimp.Model.PrintedDocument.Templates;
using Aimp.Reports.PrintedDocument.Templates;
using Aimp.Reports.Services;
using Entities;
using Nelibur.ObjectMapper;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

namespace Aimp.Logic.Services
{
    public class CashTransactionService : ICashTransactionService
    {
        public CashTransactionDocument GetDocument(int id)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                var cashTransaction = context
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
        }
        public void SaveDocument(CashTransactionDocument document,User user)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                var cashTransaction = TinyMapper.Map<CashTransaction>(document);
                if (cashTransaction.Id == 0)
                    cashTransaction.UserId = user.Id;
                context.CashTransactions.AddOrUpdate(cashTransaction);
                context.SaveChanges();
                document.Id = cashTransaction.Id;
                //document.Number = context.CashTransactions.Get(cashTransaction.Id).Number;
            }
        }
        public void DeleteDocument(CashTransactionDocument document)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                context.CashTransactions.Delete(document.Id);
                context.SaveChanges();
            }
        }
        public IQueryable<CashTransaction> GetCashTransactions(User user)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                if (user.IsAdmin())
                    return context.CashTransactions.All();
                else
                    return context.CashTransactions.All().Where(x => x.UserId == user.Id);
            }
        }
        public IQueryable<PrintedDocumentTemplate> GetPrintedDocumentTemplates()
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                string type = PrintedDocumentTemplateType.Сделка.ToString();
                return context.PrintedDocumentTemplates.All().Where(x => x.Type == type);
            }
        }
        public WordPrintedDocument GetPrintedDocument(int idTransaction, string name)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                var transaction = context.CashTransactions
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
                    context.PrintedDocumentTemplates.All().FirstOrDefault(x => x.Name == name);
                if (fileTemplate == null)
                    throw new SqlNullValueException("Шаблон не найден");
                var template = new CashTransactionPrintedDocumentTemplate(transaction, fileTemplate.File, fileTemplate.FileName);

                using (var printedService = IoC.Resolve<IPrintedService>())
                {
                    return (WordPrintedDocument)printedService.GetDocument(template);
                }
            }
        }
        public IEnumerable<EntityName> GetPrintedList()
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                string typeStr = PrintedDocumentTemplateType.Сделка.ToString();
                return context.PrintedDocumentTemplates.All().Where(x => x.Type == typeStr).Select(x => new EntityName()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
            }
        }
    }
}
