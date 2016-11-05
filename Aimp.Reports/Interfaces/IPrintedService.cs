using System;
using Aimp.Model.PrintedDocument;
using Aimp.Model.PrintedDocument.Templates;

namespace Aimp.Reports.Interfaces
{
    public interface IPrintedService : IDisposable
    {
        IPrintedDocument GetDocument(IPrintedDocumentTemplate template);
    }
}
