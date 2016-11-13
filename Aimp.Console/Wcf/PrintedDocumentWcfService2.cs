using System;
using System.Collections.Generic;
using System.Linq;
using Aimp.Domain;
using Aimp.Logic.Interfaces;
using Aimp.Model;
using Aimp.Model.Documents;
using Aimp.Model.PrintDocumentTemplate;
using Aimp.Model.PrintedDocument;
using Aimp.Reports.Interfaces;
using Aimp.ServiceContract.Services;
using Entities;

namespace Aimp.Console.Wcf
{
    public class PrintedDocumentWcfService2 : TransactionWcfService1,IPrintedDocumentWcfService
    {
        public WordPrintedDocument GetPrintedDocument(DocumentType type, string name, int id)
        {
            EventLog($"Get printed document type: {type}, name: {name}, id: {id}");
            try
            {
                switch (type)
                {
                    case DocumentType.CashTransaction:
                    {
                        var service = IoC.Resolve<ICashTransactionService>();

                        return service.GetPrintedDocument(id, name);

                    }
                    case DocumentType.CreditTransaction:
                        {
                            var service = IoC.Resolve<ICreditTransactionService>();

                        return service.GetPrintedDocument(id, name);
                        }
                    case DocumentType.Commission:
                    {
                        var service = IoC.Resolve<ICommissionService>();

                        return service.GetPrintedDocument(id, name);
                    }
                    default:
                        throw new NotImplementedException();
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public IEnumerable<EntityName> GetPrintedList(DocumentType type)
        {
            EventLog($"Get printed list type: {type}");
            try
            {
                switch (type)
                {
                    case DocumentType.CashTransaction:
                    {
                        var service = IoC.Resolve<ICashTransactionService>();

                        return service.GetPrintedList();
                    }
                    case DocumentType.CreditTransaction:
                        {
                            var service = IoC.Resolve<ICreditTransactionService>();

                            return service.GetPrintedList();
                        }
                    case DocumentType.Commission:
                        {
                            var service = IoC.Resolve<ICommissionService>();

                            return service.GetPrintedList();
                        }
                    default:
                        throw new NotImplementedException();
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public PrintedDocumentTemplate GetPrintedDocTemplate(int id)
        {
            EventLog($"Get printed document template id: {id}");
            try
            {
                return IoC.Resolve<IDocumentTemplateService>().GetTemplate(id);;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public IEnumerable<PrinDocTempListItem> GetPrintedDocTemplatesList()
        {
            EventLog("Get printed doc templates");
            try
            {
                return IoC.Resolve<IDocumentTemplateService>().GetTemplates()
                    .Select(x => new PrinDocTempListItem()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Type = x.Type
                    });
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public void SavePrintedDocTemplate(PrintedDocumentTemplate template)
        {
            EventLog($"Save printed doc template id: {template.Id}");
            try
            {
                IoC.Resolve<IDocumentTemplateService>().SaveTemplate(template);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public void DeletePrintedDocTemplate(PrintedDocumentTemplate template)
        {
            EventLog($"Delete printed doc template id: {template.Id}");
            try
            {
                IoC.Resolve<IDocumentTemplateService>().DeleteTemplate(template);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }
    }
}