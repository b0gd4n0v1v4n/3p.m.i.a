using Aimp.Entities;
using System.ServiceModel;

namespace Aimp.ServiceContracts.CashTransactions
{
    [ServiceContract(ConfigurationName = "Aimp.ICashTransactionsContract")]
    public interface ICashTransactionsContract
    {
        [OperationContract]
        CashTransactionsDto GetCashTransactions();

        [OperationContract]
        CashTransactionDto GetCashTransaction(int id);

        [OperationContract]
        void SaveCashTransaction(ICashTransaction document);

        [OperationContract]
        void DeleteCashTransaction(ICashTransaction document);
    }
}
