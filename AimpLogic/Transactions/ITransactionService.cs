using Models.ContractorInfo;
using Models.Entities;
using Models.TrancportInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AimpLogic.Transactions
{
    public interface ITransactionService: IDisposable
    {
        void SaveContractor(Contractor contractor);
        void SaveTrancport(Trancport trancport);
        TrancportInfo GetTrancportInfo();
        ContractorInfo GetContractorInfo();
        IQueryable<Contractor> GetContractors();
        IQueryable<Trancport> GetTrancports();
        UserFile GetUserFile(int id);
    }
}
