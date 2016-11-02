using Entities;
using System.Linq;

namespace Aimp.Logic.Interfaces
{
    public interface IDocumentTemplateService
    {
        PrintedDocumentTemplate GetTemplate(int id);
        int SaveTemplate(PrintedDocumentTemplate template);
        void DeleteTemplate(PrintedDocumentTemplate template);
        IQueryable<PrintedDocumentTemplate> GetTemplates();
    }
}
