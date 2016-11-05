using Aimp.Model;
using Aimp.Model.Documents;
using Aimp.Model.PrintedDocument;
using Aimp.Model.ReportOfClient;
using Entities;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Aimp.ServiceContract.Services
{
    [ServiceContract]
    public interface IClientReportsWcfSerive
    {
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        ClientReports GetClientReports();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        ClientReportDto GetNewClientReport();

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ClientReportDto GetClientReport(int id);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        int SaveClientReport(ClientReportDocument document);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        void DeleteClientReport(ClientReportDocument document);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ExcelPrintedDocument GetClientReportPrintedDocument(IEnumerable<Bank> banks, IEnumerable<ClientReportListItem> reports);
    }
}
