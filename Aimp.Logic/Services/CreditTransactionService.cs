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
using Aimp.Reports.Services;
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
    public class CreditTransactionService : ICreditTransactionService
    {
        private readonly IYearNumberSequence<int> _sequnce;
        private readonly object _sync = new object();

        public CreditTransactionService()
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                var beginSequnce = context.CreditTransactions
                    .All()
                    .GroupBy(x => x.Date.Year)
                    .Select(x => new { x.Key, x.OrderByDescending(m => m.Number).FirstOrDefault().Number })
                    .ToDictionary(x => x.Key, x => x.Number+1);

                _sequnce = new YearNumberSequence(beginSequnce);
            }
        }

        public IEnumerable<CreditTransaction> GetCreditTransactions(User user, params Expression<Func<CreditTransaction, object>>[] includes)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                if(user.IsAdmin())
                    return context.CreditTransactions.All(includes).ToList();
                else
                    return context.CreditTransactions.All(includes).Where(x => x.UserId == user.Id).ToList();
            }
        }
        public CreditTransactionDocument GetDocument(int id)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                var creditTransaction = context
                                .CreditTransactions
                                .Get(id, x => x.Seller.City,
                                        x => x.Seller.Region,
                                        x => x.Buyer.City,
                                        x => x.Buyer.Region,
                                        x => x.Owner.City,
                                        x => x.Owner.Region,
                                        x => x.Trancport.Model,
                                        x => x.Trancport.Make,
                                        x => x.Trancport.Category,
                                        x => x.Trancport.EngineType,
                                        x => x.Trancport.Type,
                                        x => x.Requisit,
                                        x => x.Creditor);

                var document = TinyMapper.Map<CreditTransactionDocument>(creditTransaction);
                document.AgentDocument = new UserFile() { Id = creditTransaction.AgentDocumentId.HasValue ? creditTransaction.AgentDocumentId.Value : 0 };
                document.DkpDocument = new UserFile() { Id = creditTransaction.DkpDocumentId.HasValue ? creditTransaction.DkpDocumentId.Value : 0 };

                return document;
            }
        }
        public void SaveDocument(CreditTransactionDocument document)
        {

            using (var context = IoC.Resolve<IDataContext>())
            {
                var creditTransaction = TinyMapper.Map<CreditTransaction>(document);

                if (creditTransaction.Id == 0)
                {
                    context.CreditTransactions.AddOrUpdate(creditTransaction);

                    if (document.UserId == 0)
                        throw new ArgumentException("UserId");

                    creditTransaction.UserId = document.UserId;
                    lock (_sync)
                    {
                        creditTransaction.Number = _sequnce.CurrentValue(creditTransaction.Date);
                        context.SaveChanges();
                        _sequnce.NextValue(creditTransaction.Date);
                    }
                    document.Id = creditTransaction.Id;
                    document.Number = creditTransaction.Number;
                }
                else
                {
                    var dbTransaction = context.CreditTransactions.Get(creditTransaction.Id, x => x.DkpDocument, x => x.AgentDocument);

                    context.UserFileUpdate(creditTransaction.DkpDocumentId, creditTransaction.DkpDocument, dbTransaction.DkpDocument);
                    context.UserFileUpdate(creditTransaction.AgentDocumentId, creditTransaction.AgentDocument, dbTransaction.AgentDocument);
                    context.SaveChanges();

                    context.CreditTransactions.AddOrUpdate(creditTransaction);
                    context.SaveChanges();
                }
            }
        }
        public void DeleteDocument(CreditTransactionDocument document)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                context.CreditTransactions.Delete(document.Id);
                context.SaveChanges();
            }
        }
        public IEnumerable<PrintedDocumentTemplate> GetPrintedDocumentTemplates()
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                string type = PrintedDocumentTemplateType.Кредит.ToString();
                string typeDkp = PrintedDocumentTemplateType.Дкп.ToString();
                string typeAkt = PrintedDocumentTemplateType.Акт.ToString();
                return context.PrintedDocumentTemplates
                    .All()
                    .Where(x => x.Type == type || x.Type == typeDkp || x.Type == typeAkt)
                    .ToList();
            }
        }
        public IEnumerable<EntityName> GetPrintedList()
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                string typeStr = PrintedDocumentTemplateType.Кредит.ToString();
                var result = context.PrintedDocumentTemplates.All().Where(x => x.Type == typeStr).Select(x => new EntityName()
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
        }
        public WordPrintedDocument GetPrintedDocument(int idTransaction, string name)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                var transaction = context.CreditTransactions
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
                                    x => x.Creditor,
                                    x => x.Requisit)
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
                        template = context.PrintedDocumentTemplates
                            .All()
                            .FirstOrDefault(x => x.Name == creditorName && x.Type == typeAkt);

                    }
                    else
                    {
                        string typeDkp = PrintedDocumentTemplateType.Дкп.ToString();
                        template = context.PrintedDocumentTemplates
                            .All()
                            .FirstOrDefault(x => x.Name == creditorName && x.Type == typeDkp);
                    }
                }
                else
                {
                    template = context.PrintedDocumentTemplates
                                        .All()
                                        .FirstOrDefault(x => x.Name == name);
                }


                if (template == null)
                    throw new SqlNullValueException("Шаблон не найден");
                var resurl = new CreditTransactionPrintedDocumentTemplate(transaction, template.File, template.FileName);

                using (var printedService = IoC.Resolve<IPrintedService>())
                {
                    return (WordPrintedDocument)printedService.GetDocument(resurl);
                }
            }
        }
        public IEnumerable<Creditor> GetCreditors()
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                return context.Creditors.All().ToList();
            }
        }
        public IEnumerable<Requisit> GetRequisits()
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                return context.Requisits.All().ToList();
            }
        }
    }
}
