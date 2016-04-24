using System;

namespace AIMP_v3._0.DataAccess
{
    public static class ConnectionSettings
    {
        public static string Address { get; set; }
        public static  Uri UriService { get { return new Uri(Address); } }

        public static string Login { get; set; } 

        public static string Password { get; set; }
    }
}
