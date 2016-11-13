using System.ServiceModel;
using Aimp.Model.Documents;

namespace Aimp.ServiceContract.Services
{
    [ServiceContract(ConfigurationName = "Service")]
    [ServiceKnownType(typeof(DocumentType))]
    public interface IAimpWcfService : ICashTransactionWcfService, ICreditTransactionWcfService, 
        ICommisionWcfService, IClientReportsWcfSerive, IDataContextWcfService, IAimpUsersWcfService,
        IPrintedDocumentWcfService,ITransactionWcfService,ICardTrancportsWcfService
    {

    }
}
