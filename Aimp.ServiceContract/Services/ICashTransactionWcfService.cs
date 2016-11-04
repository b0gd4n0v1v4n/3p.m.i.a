using System.Collections.Generic;
using Aimp.Model;
using Aimp.Model.CashTransact;
using Aimp.Model.Documents;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Aimp.ServiceContract.Services
{
    [ServiceContract]
    public interface ICashTransactionWcfService
    {
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<CashTransactionListItem> GetCashTransactions();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        CashTransactionDto GetCashTransaction(int id);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        KeyValue<int, int> SaveCashTransaction(CashTransactionDocument document);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        void DeleteCashTransaction(CashTransactionDocument document);
    }
}
