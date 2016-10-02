using Aimp.ServiceContracts.CommissionTransactions;
using System;
using Aimp.Entities;
using System.Collections.Generic;
using Aimp.ServiceContracts;

namespace AIMP_v3._0.Aimp.Services.Clients
{
    public class CommissionTransactionsClient : ICommissionTransactionsContract, IDisposable
    {
        public void DeleteCommission(ICommissionTransaction document)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public CommissionDto GetCommission(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CommissionListItem> GetCommissions()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ISourceTrancport> GetSourceTrancport()
        {
            throw new NotImplementedException();
        }

        public SaveEntityResult SaveCommission(ICommissionTransaction document)
        {
            throw new NotImplementedException();
        }
    }
}
