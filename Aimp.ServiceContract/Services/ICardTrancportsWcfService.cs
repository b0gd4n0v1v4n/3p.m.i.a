using System.Collections.Generic;
using Aimp.Model.CardTrancports;
using Aimp.Model.Documents;
using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using Entities;

namespace Aimp.ServiceContract.Services
{
    [ServiceContract]
    public interface ICardTrancportsWcfService
    {
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        CardTrancportsDto GetCardsTrancport();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        CardTrancportDocument GetCardTrancport(int id);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        int AddCardTrancport(int idCommission, DateTime dateStart);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        void SaveCardTrancport(CardTrancportDocument document);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json)]
        void DeleteCardTrancport(int idCommission);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<StatusCardTrancport> GetStatusesCardTrancport();
    }
}
