using Aimp.ServiceContracts.Dictionaries;
using System;
using System.Collections.Generic;

namespace AIMP_v3._0.Aimp.Services.Clients
{
    public class DictionariesClient : IDictionariesContract, IDisposable
    {
        public void DeleteRowDictionary(string tableName, int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Row> GetDictionary(string tableName, IEnumerable<string> columns)
        {
            throw new NotImplementedException();
        }

        public void SaveRowDictionary(string tableName, string value, int id)
        {
            throw new NotImplementedException();
        }

        public void SaveRowValuesDictionary(string tableName, IDictionary<string, string> columnValues, int id)
        {
            throw new NotImplementedException();
        }
    }
}
