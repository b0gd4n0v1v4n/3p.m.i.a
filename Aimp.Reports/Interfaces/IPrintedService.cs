using System;
using Aimp.Model.PrintedDocument;
using Aimp.Model.PrintedDocument.Templates;

namespace Aimp.Reports.Interfaces
{
    public interface IPrintedService : IDisposable
    {
#warning проверить можно ли сделать его синглетонов в IoC
        IPrintedDocument GetDocument(IPrintedDocumentTemplate template);
    }
}
