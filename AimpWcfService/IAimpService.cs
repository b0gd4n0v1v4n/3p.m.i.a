using Models;
using Models.CashTransact;
using Models.Documents;
using Models.DTO;
using Models.ReportOfClient;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace AimpWcfService
{
    public interface IAimpService
    {
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        ClientReports GetClientReports();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        RegionsDto GetRegions();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        ClientReport GetNewClientReport();

        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ClientReport GetClientReport(int id);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        Response SaveClientReport(ClientReportDocument document);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        CashTransactions GetCashTransactions();
    }
}
