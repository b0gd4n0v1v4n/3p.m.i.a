using Aimp.ServiceContracts.CreditTransactions;
using System;
using Aimp.Entities;
using System.Collections.Generic;
using Aimp.ServiceContracts;

namespace AIMP_v3._0.Aimp.Services.Clients
{
    public class CreditTransactionsClient : ICreditTransactionsContract, IDisposable
    {
        public void DeleteCreditTransaction(ICreditTransaction document)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ICreditTransaction GetCreditTransaction(int id)
        {
            throw new NotImplementedException();
        }

        public CreditTransactionInfoDto GetCreditTransactionInfo()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CreditTransactionListItem> GetCreditTransactions()
        {
            throw new NotImplementedException();
        }

        public SaveEntityResult SaveCreditTransaction(ICreditTransaction document)
        {
            throw new NotImplementedException();
        }
    }
}
