using Aimp.Entities;
using System;
using System.Linq;

namespace AimpLogic.Transactions
{
    public interface ITransactionService: IDisposable
    {
        void SaveContractor(IContractor contractor);
        void SaveTrancport(ITrancport trancport);
        TrancportInfo GetTrancportInfo();
        ContractorInfo GetContractorInfo();
        IQueryable<IContractor> GetContractors();
        IQueryable<ITrancport> GetTrancports();
        IUserFile GetUserFile(int id);
    }
}
