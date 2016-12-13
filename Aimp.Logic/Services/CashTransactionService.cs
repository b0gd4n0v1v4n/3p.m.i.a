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

namespace Aimp.Logic.Services
{
    public class CashTransactionService : ICashTransactionService
    {
        private readonly IYearNumberSequence<int> _sequnce;
        private readonly object _sync = new object();

        public CashTransactionService()
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                var beginSequnce = context.CashTransactions
                    .All()
                    .GroupBy(x => x.Date.Year)
                    .Select(x=> new { x.Key, x.OrderByDescending(m => m.Number).FirstOrDefault().Number })
                    .ToDictionary(x => x.Key, x=>x.Number + 1);

                _sequnce = new YearNumberSequence(beginSequnce);
            }
        }
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
        public void SaveDocument(CashTransactionDocument document)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                var cashTransaction = TinyMapper.Map<CashTransaction>(document);
                if (cashTransaction.Id == 0)
                    cashTransaction.UserId = document.UserId;

                context.CashTransactions.AddOrUpdate(cashTransaction);

                if (cashTransaction.Id == 0)
                {
                    if (document.UserId == 0)
                        throw new ArgumentException("UserId");

                    lock (_sync)
                    {
                        cashTransaction.Number = _sequnce.CurrentValue(cashTransaction.Date);
                        context.SaveChanges();
                        _sequnce.NextValue(cashTransaction.Date);
                    }
                    document.Id = cashTransaction.Id;
                    document.Number = cashTransaction.Number;
                }
                else
                    context.SaveChanges();
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
        public IEnumerable<CashTransaction> GetCashTransactions(User user, params Expression<Func<CashTransaction, object>>[] includes)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                if (user.IsAdmin())
                    return context.CashTransactions
                        .All(includes)
                        .ToList();
                else
                    return context.CashTransactions
                        .All(includes)
                             .ToList();
            }
        }
        public IEnumerable<PrintedDocumentTemplate> GetPrintedDocumentTemplates()
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                string type = PrintedDocumentTemplateType.Сделка.ToString();
                return context.PrintedDocumentTemplates.All().Where(x => x.Type == type).ToList();
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
