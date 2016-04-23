using System;
using Models.PrintedDocument;
using Models.PrintedDocument.Templates;

namespace AimpReports.Services
{
    public interface IPrintedDocumentService : IDisposable
    {
        IPrintedDocument GetDocument(IPrintedDocumentTemplate template);
    }
}
