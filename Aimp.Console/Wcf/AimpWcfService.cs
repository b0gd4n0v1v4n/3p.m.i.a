using Aimp.Model.CashTransact;
using Aimp.ServiceContract.Services;
using Entities;
using System.ServiceModel;

namespace Aimp.Console.Wcf
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                 ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class AimpWcfService : ClientReportsWcfSerive9, IAimpWcfService
    {

    }
}
