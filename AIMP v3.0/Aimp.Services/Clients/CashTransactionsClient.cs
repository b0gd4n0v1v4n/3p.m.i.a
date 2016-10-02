using System;
using System.Collections.Generic;
using Aimp.Entities;
using Aimp.ServiceContracts.CashTransactions;
using Aimp.ServiceContracts;

namespace AIMP_v3._0.Aimp.Services.Clients
{
    public class CashTransactionsClient : ICashTransactionsContract, IDisposable
    {
        public void DeleteCashTransaction(ICashTransaction document)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public CashTransactionDto GetCashTransaction(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CashTransactionListItem> GetCashTransactions()
        {
            throw new NotImplementedException();
        }

        public SaveEntityResult SaveCashTransaction(ICashTransaction document)
        {
            throw new NotImplementedException();
        }
    }
}
