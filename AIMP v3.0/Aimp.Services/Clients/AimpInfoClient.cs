using System;
using System.Collections.Generic;
using Aimp.Entities;
using Aimp.ServiceContracts;
using Aimp.ServiceContracts.AimpInfo;

namespace AIMP_v3._0.Aimp.Services.Clients
{
    public class AimpInfoClient : IAimpInfoContract,IDisposable
    {
        public void DeleteUser(IUser user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ContractorInfo GetContractorInfo()
        {
            throw new NotImplementedException();
        }

        public ITrancport GetTrancport(int id)
        {
            throw new NotImplementedException();
        }

        public TrancportInfo GetTrancportInfo()
        {
            throw new NotImplementedException();
        }

        public IUserFile GetUserFile(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IUserRight> GetUserRights(int idUser)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IUser> GetUsers()
        {
            throw new NotImplementedException();
        }

        public SaveEntityResult SaveContractor(IContractor contractor)
        {
            throw new NotImplementedException();
        }

        public SaveEntityResult SaveTrancport(ITrancport trancport)
        {
            throw new NotImplementedException();
        }

        public SaveEntityResult SaveUser(IUser user, IEnumerable<string> rightIds)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IContractor> SearchContractors(TypeSearchContractor type, string text)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITrancport> SearchTrancports(TypeSearchTrancport type, string text)
        {
            throw new NotImplementedException();
        }
    }
}
