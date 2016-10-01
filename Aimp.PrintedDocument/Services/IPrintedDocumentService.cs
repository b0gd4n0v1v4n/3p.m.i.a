using Aimp.Entities;
using System.Collections.Generic;

namespace Aimp.PrintedDocument
{
    public interface IPrintedDocumentService
    {
        IPrintedDocumentTemplate GetTemplate(int id);
        int SaveTemplate(IPrintedDocumentTemplate template);
        void DeleteTemplate(IPrintedDocumentTemplate template);
        IEnumerable<IPrintedDocumentTemplate> GetTemplates();
    }
}
