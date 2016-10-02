using Aimp.ServiceContracts.PrintedDocument;
using System;
using Aimp.Entities;
using Aimp.Model.PrintedDocument;
using Aimp.ServiceContracts;
using System.Collections.Generic;

namespace AIMP_v3._0.Aimp.Services.Clients
{
    public class PrintedDocumentClient : IPrintedDocumentContract, IDisposable
    {
        public void DeletePrintedDocTemplate(IPrintedDocumentTemplate template)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IPrintedDocumentTemplate GetPrintedDocTemplate(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PrinDocTempListItem> GetPrintedDocTemplatesList()
        {
            throw new NotImplementedException();
        }

        public WordPrintedDocument GetPrintedDocument(DocumentType type, string name, int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntityName> GetPrintedList(DocumentType name)
        {
            throw new NotImplementedException();
        }

        public void SavePrintedDocTemplate(IPrintedDocumentTemplate template)
        {
            throw new NotImplementedException();
        }
    }
}
