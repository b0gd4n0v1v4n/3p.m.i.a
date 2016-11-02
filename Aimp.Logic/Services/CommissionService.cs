using Aimp.DataAccess.Interfaces;
using Aimp.Domain;
using Aimp.Logic.Extensions;
using Aimp.Logic.Interfaces;
using Aimp.Model;
using Aimp.Model.Documents;
using Aimp.Model.PrintedDocument;
using Aimp.Model.PrintedDocument.Templates;
using Aimp.Reports.Services;
using Aimp.Reports.Templates;
using Entities;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aimp.Logic.Services
{
    public class CommissionService : ICommissionService
    {
        public CommissionDocument GetDocument(int id)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                var commission = context
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
        }
        public void SaveDocument(CommissionDocument document,User user)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                var commission = TinyMapper.Map<CommissionTransaction>(document);
                if (commission.Id == 0)
                    commission.UserId = user.Id;
                context.CommissionTransactions.AddOrUpdate(commission);
                context.SaveChanges();
                document.Id = commission.Id;
                //document.Number = context.CommissionTransactions.Get(commission.Id).Number;
            }
        }
        public void DeleteDocument(CommissionDocument document)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                context.CommissionTransactions.Delete(document.Id);
                var dleteCardIds = context.CardsTrancport
                    .All()
                    .Where(x => x.CommissionTransaction.Id == document.Id)
                    .Select(x => x.Id).ToArray();
                context.CardsTrancport
                    .DeleteRange(dleteCardIds);
                context.SaveChanges();
            }
        }
        public IQueryable<CommissionTransaction> GetCommissions(User user)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                if (user.IsAdmin())
                    return context.CommissionTransactions.All();
                else
                    return context.CommissionTransactions.All().Where(x => x.UserId == user.Id);
            }
        }
        public IQueryable<PrintedDocumentTemplate> GetPrintedDocumentTemplates()
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                string type = PrintedDocumentTemplateType.Комиссия.ToString();
                return context.PrintedDocumentTemplates.All().Where(x => x.Type == type);
            }
        }
        public WordPrintedDocument GetPrintedDocument(int idCommission, string name)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                var transaction = context.CommissionTransactions
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
                    context.PrintedDocumentTemplates.All().FirstOrDefault(x => x.Name == name);
                if (fileTemplate == null)
                    throw new SqlNullValueException("Шаблон не найден");

                var template = new CommissionTransactionPrintedDocumentTemplate(transaction, fileTemplate.File, fileTemplate.FileName);

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
                string typeStr = PrintedDocumentTemplateType.Комиссия.ToString();
                return context.PrintedDocumentTemplates.All().Where(x => x.Type == typeStr).Select(x => new EntityName()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
            }
        }

        public IQueryable<SourceTrancport> GetSourcesTrancport()
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                return context.SourcesTrancport.All();
            }
        }
    }
}
