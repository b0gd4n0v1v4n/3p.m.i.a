using Aimp.DataAccess.Interfaces;
using Aimp.Domain;
using Aimp.Logic.Extensions;
using Aimp.Logic.Interfaces;
using Aimp.Logic.Sequnces;
using Aimp.Model;
using Aimp.Model.Documents;
using Aimp.Model.PrintedDocument;
using Aimp.Model.PrintedDocument.Templates;
using Aimp.Reports.Interfaces;
using Aimp.Reports.Templates;
using Entities;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Aimp.Logic.Services
{
    public class CommissionService : ICommissionService
    {
        private readonly IYearNumberSequence<int> _sequnce;
        private readonly object _sync = new object();

        public CommissionService()
        {
#warning ПРОВЕРИТЬ
            using (var context = IoC.Resolve<IDataContext>())
            {
                var beginSequnce = context.CommissionTransactions
                    .All()
                    .GroupBy(x => x.Date.Year)
                    .Select(x => new { x.Key, x.OrderByDescending(m => m.Number).FirstOrDefault().Number })
                    .ToDictionary(x => x.Key, x => x.Number);

                _sequnce = new YearNumberSequence(beginSequnce);
            }
        }

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
        public void SaveDocument(CommissionDocument document)
        {
            if (document.UserId == 0)
                throw new ArgumentException("UserId");

            using (var context = IoC.Resolve<IDataContext>())
            {
                var commission = TinyMapper.Map<CommissionTransaction>(document);
                if (commission.Id == 0)
                    commission.UserId = document.UserId;
                
                context.CommissionTransactions.AddOrUpdate(commission);

                lock (_sync)
                {
                    commission.Number = _sequnce.CurrentValue(commission.Date);
                    context.SaveChanges();
                    _sequnce.NextValue(commission.Date);
                }

                document.Id = commission.Id;
                document.Number = commission.Number;
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
        public IEnumerable<CommissionTransaction> GetCommissions(User user, params Expression<Func<CommissionTransaction, object>>[] includes)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                if (user.IsAdmin())
                    return context.CommissionTransactions.All(includes).ToList();
                else
                    return context.CommissionTransactions.All(includes).Where(x => x.UserId == user.Id).ToList();
            }
        }
        public IEnumerable<PrintedDocumentTemplate> GetPrintedDocumentTemplates()
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                string type = PrintedDocumentTemplateType.Комиссия.ToString();
                return context.PrintedDocumentTemplates.All().Where(x => x.Type == type).ToList();
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

        public IEnumerable<SourceTrancport> GetSourcesTrancport()
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                return context.SourcesTrancport.All().ToList();
            }
        }
    }
}
