using Aimp.Entities;
using System.Collections.Generic;
using System.ServiceModel;

namespace Aimp.ServiceContracts.CashTransactions
{
    [ServiceContract(ConfigurationName = "Aimp.ICashTransactionsContract")]
    public interface ICashTransactionsContract
    {
        [OperationContract]
        IEnumerable<CashTransactionListItem> GetCashTransactions();

        [OperationContract]
        CashTransactionDto GetCashTransaction(int id);

        [OperationContract]
        void SaveCashTransaction(ICashTransaction document);

        [OperationContract]
        void DeleteCashTransaction(ICashTransaction document);
    }
}
