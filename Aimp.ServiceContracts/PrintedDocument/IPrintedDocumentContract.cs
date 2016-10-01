using Aimp.Entities;
using Aimp.Model.PrintedDocument;
using System.Collections.Generic;
using System.ServiceModel;

namespace Aimp.ServiceContracts.PrintedDocument
{
    [ServiceContract(ConfigurationName = "Aimp.IPrintedDocumentContract")]
    public interface IPrintedDocumentContract
    {
        [OperationContract]
        WordPrintedDocument GetPrintedDocument(DocumentType type, string name, int id);

        [OperationContract]
        IEnumerable<EntityName> GetPrintedList(DocumentType name);

        [OperationContract]
        IPrintedDocumentTemplate GetPrintedDocTemplate(int id);

        [OperationContract]
        IEnumerable<PrinDocTempListItem> GetPrintedDocTemplatesList();

        [OperationContract]
        void SavePrintedDocTemplate(IPrintedDocumentTemplate template);

        [OperationContract]
        void DeletePrintedDocTemplate(IPrintedDocumentTemplate template);
    }
}
