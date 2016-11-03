using System.Collections.Generic;
using Aimp.Model.ContractorInfo;
using Aimp.Model.TrancportInfo;
using Entities;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Aimp.ServiceContract.Services
{
    [ServiceContract]
    public interface ITransactionWcfService
    {
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        TrancportInfo GetTrancportInfo();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        ContractorInfo GetContractorInfo();

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<Contractor> SearchContractors(TypeSearchContractor type, string text);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<Trancport> SearchTrancports(TypeSearchTrancport type, string text);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void SaveContractor(Contractor contractor);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void SaveTrancport(Trancport trancport);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        UserFile GetUserFile(int id);
    }
}
