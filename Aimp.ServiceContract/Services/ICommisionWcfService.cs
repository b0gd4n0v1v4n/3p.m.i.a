using System.Collections.Generic;
using Aimp.Model.Commission;
using Aimp.Model.Documents;
using System.ServiceModel;
using System.ServiceModel.Web;
using Entities;

namespace Aimp.ServiceContract.Services
{
    public interface ICommisionWcfService
    {
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<CommissionListItem> GetCommissions();
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<SourceTrancport> GetSourceTrancport();
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        CommissionDto GetCommission(int id);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        int SaveCommission(CommissionDocument document);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        void DeleteCommission(CommissionDocument document);
    }
}
