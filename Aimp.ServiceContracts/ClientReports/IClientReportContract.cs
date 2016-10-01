using Aimp.Entities;
using Aimp.Model.PrintedDocument;
using System.ServiceModel;

namespace Aimp.ServiceContracts.ClientReports
{
    [ServiceContract(ConfigurationName = "Aimp.IClientReportContract")]
    public interface IClientReportContract
    {
        [OperationContract]
        ClientReports GetClientReports();

        [OperationContract]
        IClientReport GetNewClientReport();

        [OperationContract]
        void GetClientReport(int id);

        [OperationContract]
        void SaveClientReport(IClientReport document);

        [OperationContract]
        void DeleteClientReport(IClientReport document);
        [OperationContract]
        ExcelPrintedDocument GetClientReportPrintedDocument(ClientReports reports);
    }
}
