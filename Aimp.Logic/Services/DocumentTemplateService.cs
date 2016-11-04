using System.Collections.Generic;
using Aimp.DataAccess.Interfaces;
using Aimp.Domain;
using Aimp.Logic.Interfaces;
using Entities;
using System.Linq;

namespace Aimp.Logic.Services
{
    public class DocumentTemplateService : IDocumentTemplateService
    {
        public void DeleteTemplate(PrintedDocumentTemplate template)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                context.PrintedDocumentTemplates.Delete(template);
                context.SaveChanges();
            }
        }

        public PrintedDocumentTemplate GetTemplate(int id)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                return context.PrintedDocumentTemplates.Get(id);
            }
        }

        public IEnumerable<PrintedDocumentTemplate> GetTemplates()
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                return context.PrintedDocumentTemplates.All().ToList();
            }
        }

        public int SaveTemplate(PrintedDocumentTemplate template)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                context.PrintedDocumentTemplates.AddOrUpdate(template);
                context.SaveChanges();
                return template.Id;
            }
        }
    }
}
