using Models.Entities;
using System;
using System.Linq;

namespace AimpLogic.PrintedDocument
{
    public interface IPrintedDocumentService : IDisposable
    {
        PrintedDocumentTemplate GetTemplate(int id);
        int SaveTemplate(PrintedDocumentTemplate template);
        void DeleteTemplate(PrintedDocumentTemplate template);
        IQueryable<PrintedDocumentTemplate> GetTemplates();
    }
}
