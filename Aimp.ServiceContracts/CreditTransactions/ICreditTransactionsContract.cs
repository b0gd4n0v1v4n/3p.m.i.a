using Aimp.Entities;
using System.Collections.Generic;
using System.ServiceModel;

namespace Aimp.ServiceContracts.CreditTransactions
{
    [ServiceContract(ConfigurationName = "Aimp.ICreditTransactionsContract")]
    public interface ICreditTransactionsContract
    {
        [OperationContract]
        IEnumerable<CreditTransactionListItem> GetCreditTransactions();

        [OperationContract]
        ICreditTransaction GetCreditTransaction(int id);

        [OperationContract]
        void SaveCreditTransaction(ICreditTransaction document);

        [OperationContract]
        void DeleteCreditTransaction(ICreditTransaction document);

        [OperationContract]
        CreditTransactionInfoDto GetCreditTransactionInfo();
    }
}
