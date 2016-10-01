using System.Collections.Generic;
using System.ServiceModel;

namespace Aimp.ServiceContracts.Dictionaries
{
    [ServiceContract(ConfigurationName = "Aimp.IDictionariesContract")]
    public interface IDictionariesContract
    {
        [OperationContract]
        IEnumerable<Row> GetDictionary(string tableName, IEnumerable<string> columns);

        [OperationContract]
        void SaveRowDictionary(string tableName, string value, int id);

        [OperationContract]
        void DeleteRowDictionary(string tableName, int id);

        [OperationContract]
        void SaveRowValuesDictionary(string tableName, IDictionary<string, string> columnValues, int id);
    }
}
