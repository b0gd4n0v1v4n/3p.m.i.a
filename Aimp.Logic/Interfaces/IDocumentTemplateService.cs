using System.Collections.Generic;
using Entities;

namespace Aimp.Logic.Interfaces
{
    public interface IDocumentTemplateService
    {
        PrintedDocumentTemplate GetTemplate(int id);
        int SaveTemplate(PrintedDocumentTemplate template);
        void DeleteTemplate(PrintedDocumentTemplate template);
        IEnumerable<PrintedDocumentTemplate> GetTemplates();
    }
}
