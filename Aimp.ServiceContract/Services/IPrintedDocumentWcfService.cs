using System.Collections.Generic;
using Aimp.Model;
using Aimp.Model.Documents;
using Aimp.Model.PrintDocumentTemplate;
using Aimp.Model.PrintedDocument;
using Entities;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Aimp.ServiceContract.Services
{
    [ServiceContract]
    public interface IPrintedDocumentWcfService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        WordPrintedDocument GetPrintedDocument(DocumentType type, string name, int id);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<EntityName> GetPrintedList(DocumentType name);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PrintedDocumentTemplate GetPrintedDocTemplate(int id);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<PrinDocTempListItem> GetPrintedDocTemplatesList();

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        void SavePrintedDocTemplate(PrintedDocumentTemplate template);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void DeletePrintedDocTemplate(PrintedDocumentTemplate template);
    }
}
