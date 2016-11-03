    using Aimp.Model;
using Aimp.Model.Documents;
using Aimp.Model.PrintedDocument;
using Aimp.Model.ReportOfClient;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Aimp.ServiceContract.Services
{
    public interface IClientReportsWcfSerive
    {
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        ClientReports GetClientReports();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        ClientReport GetNewClientReport();

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ClientReport GetClientReport(int id);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        void SaveClientReport(ClientReportDocument document);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        void DeleteClientReport(ClientReportDocument document);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ExcelPrintedDocument GetClientReportPrintedDocument(ClientReports reports);
    }
}
