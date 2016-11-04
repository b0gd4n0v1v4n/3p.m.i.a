using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Aimp.Model.ContractorInfo;
using Aimp.Model.TrancportInfo;
using Entities;

namespace Aimp.Logic.Interfaces
{
    public interface ITransactionService
    {
        void SaveContractor(Contractor contractor);
        void SaveTrancport(Trancport trancport);
        TrancportInfo GetTrancportInfo();
        ContractorInfo GetContractorInfo();
        IEnumerable<Contractor> GetContractors(Expression<Func<Contractor, bool>> predicate = null);
        IEnumerable<Trancport> GetTrancports(Expression<Func<Trancport, bool>> predicate = null);
        UserFile GetUserFile(int id);
    }
}