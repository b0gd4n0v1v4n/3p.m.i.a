using Aimp.Entities;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Aimp.ServiceContracts.TrancportCards
{
    [ServiceContract(ConfigurationName = "Aimp.ITrancportCardsContract")]
    public interface ITrancportCardsContract
    {
        [OperationContract]
        CardTrancportsDto GetCardsTrancport();

        [OperationContract]
        CardTrancportDocument GetCardTrancport(int id);

        [OperationContract]
        int AddCardTrancport(int idCommission, DateTime dateStart);

        [OperationContract]
        void SaveCardTrancport(CardTrancportDocument document);

        [OperationContract]
        void DeleteCardTrancport(int id);

        [OperationContract]
        IEnumerable<IStatusCardTrancport> GetStatusesCardTrancport();
    }
}
