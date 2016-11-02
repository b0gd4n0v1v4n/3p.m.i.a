using Aimp.Model.PrintedDocument;
using Aimp.Model.PrintedDocument.Templates;
using System;

namespace Aimp.Reports.Services
{
    public interface IPrintedService : IDisposable
    {
#warning проверить можно ли сделать его синглетонов в IoC
        IPrintedDocument GetDocument(IPrintedDocumentTemplate template);
    }
}
