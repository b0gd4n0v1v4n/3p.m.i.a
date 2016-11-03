using System;
using System.Collections.Generic;
using System.Linq;
using Aimp.DataAccess.Interfaces;
using Aimp.Domain;
using Aimp.Model;
using Aimp.Model.Dictionar;
using Aimp.ServiceContract.Services;

namespace Aimp.Console.Wcf
{
    public class DataContextWcfService3 : PrintedDocumentWcfService2, IDataContextWcfService
    {
        public IEnumerable<Row> GetDictionary(string tableName, IEnumerable<string> columns)
        {
            EventLog($"Get dicionary name: {tableBame}") ;
            try
            {
                IEnumerable<EntityName> query;

                using (var service = IoC.Resolve<IDataContext>())
                    query = service.Query<EntityName>($"SELECT Id,CONCAT({nameColumns}) as Name FROM {tableName}");

                List<Row> result = new List<Row>();
                foreach(var iRow in query)
                {
                    Row row = new Row(tableName);
                    var cells = iRow.Name.Split(new[] { "$#$" }, StringSplitOptions.None);
                    for (int iColumn = 0; iColumn < columns.Count(); iColumn++)
                    {
                        row.Cells.Add(new KeyValue<string, string>() { Key = columns.Skip(iColumn).First(), Value = cells[iColumn] });
                    }
                    result.Add(row);
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public void DeleteRowDictionary(string tableName, int id)
        {
            EventLog($"Delete id: '{id}' from dictionary: {tableName}");
            try
            {
                using (var service = IoC.Resolve<IDataContext>())
                    service.Command($"DELETE FROM {table} WHERE [Id] = {id}");
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public void SaveRowValuesDictionary(string tableName, IDictionary<string, string> columnValues, int id)
        {
            EventLog($"Save id: {id}, dictionary: {tableName}");
            try
            {
                string query = string.Empty;
            if(id == 0)
            {
                query = $"INSERT INTO [{table}](";
                string columns = string.Empty;
                string values = string.Empty;
                foreach(var iColum in columnValues)
                {
                    if (iColum.Key != "Id")
                    {
                        columns = $"{columns},[{iColum.Key}]";
                        values = $"{values},'{iColum.Value}'";
                    }
                }
                query = $"{query + columns.Substring(1)}) VALUES ({values.Substring(1)})";
            }
            else
            {
                query = $"UPDATE [{table}] SET ";
                foreach (var iColum in columnValues)
                {
                    if (iColum.Key != "Id")
                        query = $"{query} {iColum.Key} = '{iColum.Value}',";
                }
                query = $"{query.Substring(0, query.Length - 1)} WHERE [Id] = {id}";
            }
            using (var service = IoC.Resolve<IDataContext>())
                    service.Command(query);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }
    }
}
