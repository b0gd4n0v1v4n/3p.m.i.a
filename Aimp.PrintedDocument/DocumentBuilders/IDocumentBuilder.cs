using Aimp.Model.PrintedDocument;
using System;

namespace Aimp.PrintedDocument.DocumentBuilders
{
    public interface IDocumentBuilder : IDisposable
    {
        IPrintedDocument Build(IPrintedDocumentTemplate template);
    }
}
