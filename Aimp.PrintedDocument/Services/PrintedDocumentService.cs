using Aimp.DataContext;
using Aimp.Entities;
using Aimp.Infrastructure.IoC;
using System.Collections.Generic;
using System.Linq;

namespace Aimp.PrintedDocument
{
    public class PrintedDocumentService : IPrintedDocumentService
    {
        public void DeleteTemplate(IPrintedDocumentTemplate template)
        {
            using (var context = IoC.Resolve<IAimpContext>())
            {
                context.PrintedDocumentTemplates.Delete(template);
                context.SaveChanges();
            }
        }

        public IPrintedDocumentTemplate GetTemplate(int id)
        {
            using (var context = IoC.Resolve<IAimpContext>())
                return context.PrintedDocumentTemplates.Get(id);
        }

        public IEnumerable<IPrintedDocumentTemplate> GetTemplates()
        {
            using (var context = IoC.Resolve<IAimpContext>())
                return context.PrintedDocumentTemplates.All().ToList();
        }

        public int SaveTemplate(IPrintedDocumentTemplate template)
        {
            using (var context = IoC.Resolve<IAimpContext>())
            {
                context.PrintedDocumentTemplates.AddOrUpdate(template);
                context.SaveChanges();
                return template.Id;
            }
        }
    }
}
