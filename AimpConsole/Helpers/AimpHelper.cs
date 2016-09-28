using AimpLogic.Transactions;
using Models;
using Models.Dictionar;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AimpConsole.Helpers
{
    public abstract class AimpHelper
    {
        public static User User { get; }
        static AimpHelper()
        {
            User = new User();
        }
        public IEnumerable<Row> GetDictionary(string tableName, IEnumerable<string> columns)
        {
            if (columns.First() != "Id")
                throw new ArgumentNullException("Id");

            string nameColumns = string.Empty;
            
            foreach (string iColumn in columns)
            {
                nameColumns = nameColumns + $",'$#$',{iColumn}";
            }
            nameColumns = nameColumns.Substring(7);
            using(var service = new TransactionService(User.Login, User.Password))
            {
                var query = service.Query<EntityName>($"SELECT Id,CONCAT({nameColumns}) as Name FROM {tableName}");
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
        }

        public void SaveRowDictionary(string table, string value,int id)
        {
            using (var service = new TransactionService(User.Login, User.Password))
            {
                if (id != 0)
                    service.Command($"UPDATE {table} SET Name = '{value}' WHERE Id = {id}");
                else
                    service.Command($"INSERT INTO {table} (Name) VALUES ('{value}')");
            }
        }
        public void SaveRowDictionary(string table,IDictionary<string,string> columnValues, int id)
        {
            string query = string.Empty;
            if(id == 0)
            {
                query = $"INSERT INTO {table}(";
                string columns = string.Empty;
                string values = string.Empty;
                foreach(var iColum in columnValues)
                {
                    columns = $"{columns},{iColum.Key}";
                    values = $"{values},{iColum.Value}";
                }
                query = $"{query + columns.Substring(0)}) VALUES ({values.Substring(0)})";
            }
            else
            {
                query = $"UPDAT {table} SET ";
                foreach (var iColum in columnValues)
                {
                    query = $"{query} {iColum.Key} = '{iColum.Value}',";
                }
                query = $"{query.Substring(0, query.Length - 1)} WHERE Id = {id}";
            }
            using (var service = new TransactionService(User.Login, User.Password))
            {
                    service.Command(query);;
            }

        }
        public void DeleteRowDictionary(string table, int id)
        {
            using (var service = new TransactionService(User.Login, User.Password))
            {
                service.Command($"DELETE FROM {table} WHERE Id = {id}");
            }
        }
    }
}
