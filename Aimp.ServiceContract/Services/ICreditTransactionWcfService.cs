using System.Collections.Generic;
using Aimp.Model.CreditTransact;
using Aimp.Model.Documents;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Aimp.ServiceContract.Services
{
    public interface ICreditTransactionWcfService
    {
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<CreditTransactionListItem> GetCreditTransactions();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        CreditTransactionDocument GetCreditTransaction(int id);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        void SaveCreditTransaction(CreditTransactionDocument document);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        void DeleteCreditTransaction(CreditTransactionDocument document);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        CreditTransactionInfoDto GetCreditTransactionInfo();
    }
}
