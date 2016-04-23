using System.Collections.Generic;
using AIMP_v3._0.Enums;
using TestAccordDb2AndDb3Version.Version2;

namespace TestAccordDb2AndDb3Version.version2
{
    public class VersionTwo : IReport
    {
        public Dictionary<string, string> Get(TypeReport type, int idTransaction)
        {
            Dictionary<string,string> result = new Dictionary<string, string>();

            string queryTemplateCommand = $"SELECT запрос FROM ТИПЫ_ШАБЛОНОВ WHERE код = {(int) type}";

            string queryTemplate = database.Query(queryTemplateCommand).Rows[0][0].ToString();

            var table = database.Query($"{queryTemplate} {idTransaction}");

            for (int iColumn = 0; iColumn < table.Columns.Count; iColumn++)
            {
                result.Add(table.Columns[iColumn].ColumnName, table.Rows[0][iColumn].ToString());
            }

            return result;
        } 
    }
}
