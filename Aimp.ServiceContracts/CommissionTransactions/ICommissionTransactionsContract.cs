using Aimp.Entities;
using System.Collections.Generic;
using System.ServiceModel;

namespace Aimp.ServiceContracts.CommissionTransactions
{
    [ServiceContract(ConfigurationName = "Aimp.ICommissionTransactionsContract")]
    public interface ICommissionTransactionsContract
    {
        [OperationContract]
        IEnumerable<CommissionListItem> GetCommissions();
        [OperationContract]
        IEnumerable<ISourceTrancport> GetSourceTrancport();
        [OperationContract]
        CommissionDto GetCommission(int id);
        [OperationContract]
        void SaveCommission(ICommissionTransaction document);
        [OperationContract]
        void DeleteCommission(ICommissionTransaction document);
    }
}
