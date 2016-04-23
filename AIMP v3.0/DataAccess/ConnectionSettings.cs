using System;

namespace AIMP_v3._0.DataAccess
{
    public static class ConnectionSettings
    {
        public static  Uri UriService { get { return new Uri("http://localhost:1980/abc"); } }

        public static string Login { get; set; } 

        public static string Password { get; set; }
    }
}
