using Aimp.Model.ContractorInfo;
using Aimp.Model.TrancportInfo;
using Entities;
using System.Linq;

namespace Aimp.Logic.Interfaces
{
    public interface ITransactionService 
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
