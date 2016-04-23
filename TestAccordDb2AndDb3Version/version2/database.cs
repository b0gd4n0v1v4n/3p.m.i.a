using System;
using DataTable = System.Data.DataTable;
using System.Data.SqlClient;

namespace TestAccordDb2AndDb3Version.Version2
{
    public class database
    {
        private static string _connectionString;
        public static string ExecuteScalar(string command)
        {
            string result;
            using (var conn = GetConnection())
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = command;
                conn.Open();
                result = Convert.ToString(cmd.ExecuteScalar());
            }
            return result;
        }
        public static string connectionString
        {
            get
            {
                SqlConnectionStringBuilder bilder = new SqlConnectionStringBuilder();
                bilder.DataSource = @"HOEM-PC\SQLEXPRESS";
                bilder.InitialCatalog = "aimp";
                bilder.UserID = "user";
                bilder.Password = "Qwerty123";
                _connectionString = bilder.ConnectionString;
                return _connectionString;
            }
            set { _connectionString = value; }
        }
        public static DataTable Query(string query)
        {
            DataTable table = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandText = query;
                conn.Open();
                using (var reader = command.ExecuteReader())
                {
                    table.Load(reader);
                }
            }
            return table;
        }
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}