using System;
using System.Collections.Generic;
using Aimp.ServiceContracts.Dictionaries;
using Aimp.Infrastructure.IoC;
using Aimp.DataContext;
using System.Linq;
using Aimp.ServiceContracts;

namespace Aimp.Wcf.Services
{
    public class DictionariesService : IDictionariesContract
    {
        public void DeleteRowDictionary(string tableName, int id)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    context.Command($"DELETE FROM {tableName} WHERE [Id] = {id}");
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public IEnumerable<Row> GetDictionary(string tableName, IEnumerable<string> columns)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    if (columns.First() != "Id")
                        throw new ArgumentNullException("Id");

                    string nameColumns = string.Empty;

                    foreach (string iColumn in columns)
                    {
                        nameColumns = nameColumns + $",'$#$',{iColumn}";
                    }
                    nameColumns = nameColumns.Substring(7);
                    var query = context.Query<EntityName>($"SELECT Id,CONCAT({nameColumns}) as Name FROM {tableName}");
                    List<Row> result = new List<Row>();
                    foreach (var iRow in query)
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
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public void SaveRowDictionary(string table, string value, int id)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    if (id != 0)
                        context.Command($"UPDATE {table} SET Name = '{value}' WHERE Id = {id}");
                    else
                        context.Command($"INSERT INTO {table} (Name) VALUES ('{value}')");
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public void SaveRowValuesDictionary(string table, IDictionary<string, string> columnValues, int id)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    string query = string.Empty;
                    if (id == 0)
                    {
                        query = $"INSERT INTO [{table}](";
                        string columns = string.Empty;
                        string values = string.Empty;
                        foreach (var iColum in columnValues)
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
                        context.Command(query);
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }
    }
}
