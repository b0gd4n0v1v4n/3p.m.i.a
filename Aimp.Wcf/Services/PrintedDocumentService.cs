using System;
using System.Collections.Generic;
using Aimp.Entities;
using Aimp.Model.PrintedDocument;
using Aimp.ServiceContracts;
using Aimp.ServiceContracts.PrintedDocument;
using Aimp.Infrastructure.IoC;
using Aimp.DataContext;
using System.Linq;
using Aimp.PrintedDocument;
using Aimp.PrintedDocument.Templates;
using Aimp.PrintedDocument.DocumentBuilders.Word;
using Aimp.Model.Entities;

namespace Aimp.Wcf.Services
{
    public class PrintedDocumentService : IPrintedDocumentContract
    {
        public void DeletePrintedDocTemplate(Entities.IPrintedDocumentTemplate template)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    context.PrintedDocumentTemplates.Delete(template);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public Entities.IPrintedDocumentTemplate GetPrintedDocTemplate(int id)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    return context.PrintedDocumentTemplates.Get(id);
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public IEnumerable<PrinDocTempListItem> GetPrintedDocTemplatesList()
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                   return context.PrintedDocumentTemplates.All()
                .Select(x => new PrinDocTempListItem()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type
                })
            .ToList();
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public WordPrintedDocument GetPrintedDocument(DocumentType type, string name, int id)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    switch (type)
                    {
                        case DocumentType.CashTransaction:
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
                                .FirstOrDefault(x => x.Id == id);
                                if (transaction == null)
                                    throw new NullReferenceException("Документ не найден");
                                var typeId = PrintedDocumentTemplateType.Сделка.ToString();
                                var fileTemplate =
                                    context.PrintedDocumentTemplates.All().FirstOrDefault(x => x.Name == name);
                                if (fileTemplate == null)
                                    throw new NullReferenceException("Шаблон не найден");
                                var template = new CashTransactionPrintedDocumentTemplate(transaction, fileTemplate.File);
                                using (var printedService = new WordPrintedDocumentService())
                                {
                                    return (WordPrintedDocument)printedService.Build(template);
                                }
                            }
                        case DocumentType.CreditTransaction:
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
                                .FirstOrDefault(x => x.Id == id);
                                if (transaction == null)
                                    throw new NullReferenceException("Документ не найден");
                                Entities.IPrintedDocumentTemplate template = null;
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
                                    throw new NullReferenceException("Шаблон не найден");
                                var resurl = new CreditTransactionPrintedDocumentTemplate(transaction, template.File);
                                using (var printedService = new WordPrintedDocumentService())
                                {
                                    return (WordPrintedDocument)printedService.Build(resurl);
                                }
                            }
                        case DocumentType.Commission:
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
                                .FirstOrDefault(x => x.Id == id);
                                if (transaction == null)
                                    throw new NullReferenceException("Документ не найден");
                                var typeId = PrintedDocumentTemplateType.Сделка.ToString();
                                var fileTemplate =
                                    context.PrintedDocumentTemplates.All().FirstOrDefault(x => x.Name == name);
                                if (fileTemplate == null)
                                    throw new NullReferenceException("Шаблон не найден");
                                var template = new CommissionTransactionPrintedDocumentTemplate(transaction, fileTemplate.File);
                                using (var printedService = new WordPrintedDocumentService())
                                {
                                    return (WordPrintedDocument)printedService.Build(template);
                                }
                            }
                        default:
                            throw new NotImplementedException();
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public IEnumerable<EntityName> GetPrintedList(DocumentType type)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    switch (type)
                    {
                        case DocumentType.CashTransaction:
                            {
                                string typeStr = PrintedDocumentTemplateType.Сделка.ToString();
                                return context.PrintedDocumentTemplates.All().Where(x => x.Type == typeStr).Select(x => new EntityName()
                                {
                                    Id = x.Id,
                                    Name = x.Name
                                }).ToList();
                            }
                        case DocumentType.CreditTransaction:
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
                        case DocumentType.Commission:
                            {
                                string typeStr = PrintedDocumentTemplateType.Комиссия.ToString();
                                return context.PrintedDocumentTemplates.All().Where(x => x.Type == typeStr).Select(x => new EntityName()
                                {
                                    Id = x.Id,
                                    Name = x.Name
                                }).ToList();
                            }
                        default:
                            throw new NotImplementedException();
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public void SavePrintedDocTemplate(Entities.IPrintedDocumentTemplate template)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    context.PrintedDocumentTemplates.AddOrUpdate(template);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }
    }
}
