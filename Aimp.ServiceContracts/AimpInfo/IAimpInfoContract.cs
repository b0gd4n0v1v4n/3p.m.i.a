using Aimp.Entities;
using System.Collections.Generic;
using System.ServiceModel;

namespace Aimp.ServiceContracts.AimpInfo
{
    [ServiceContract(ConfigurationName = "Aimp.IAimpInfoContract")]
    public interface IAimpInfoContract
    {
        //trancports
        [OperationContract]
        ITrancport GetTrancport(int id);

        [OperationContract]
        SaveEntityResult SaveTrancport(ITrancport trancport);

        [OperationContract]
        IEnumerable<ITrancport> SearchTrancports(TypeSearchTrancport type, string text);


        [OperationContract]
        TrancportInfo GetTrancportInfo();

        //contractors
        [OperationContract] 
        ContractorInfo GetContractorInfo();

        [OperationContract]
        IEnumerable<IContractor> SearchContractors(TypeSearchContractor type, string text);

        [OperationContract]
        SaveEntityResult SaveContractor(IContractor contractor);

        [OperationContract]
        IEnumerable<IUserRight> GetUserRights(int idUser);

        [OperationContract]
        IEnumerable<IUser> GetUsers();

        [OperationContract]
        SaveEntityResult SaveUser(IUser user, IEnumerable<string> rightIds);

        [OperationContract]
        void DeleteUser(IUser user);

        //user files
        [OperationContract]
        IUserFile GetUserFile(int id);
    }
}
