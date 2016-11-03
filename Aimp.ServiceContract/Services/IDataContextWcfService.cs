using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Aimp.Model.Dictionar;

namespace Aimp.ServiceContract.Services
{
    public interface IDataContextWcfService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<Row> GetDictionary(string tableName, IEnumerable<string> columns);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        void DeleteRowDictionary(string tableName, int id);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        void SaveRowValuesDictionary(string tableName, IDictionary<string, string> columnValues, int id);
    }
}