﻿using System;
using System.Linq;
using Aimp.Model.SecurityRigths;

namespace AIMP_v3._0.DataAccess
{
    public static class CurrentUser
    {
        public static string LastName { get; set; }
        public static bool IsAdmin { get; private set; }
        public static bool IsView { get; private set; }
        public static bool IsAdd { get; private set; }
        public static bool IsDelete { get; private set; }
        public static void Auth(string login,string password)
        {
            ConnectionSettings.Login = login;
            ConnectionSettings.Password = password;
            using(var service = new AimpService())
            {
                var response = service.Auth();
                LastName = response.LastName;
                if (response.UserRights.Any(x=>x == UserRightsCollection.Admin.Id))
                {
                    IsAdd = true;
                    IsAdmin = true;
                    IsView = true;
                    IsDelete = true;
                }
                else
                {
                    if(response.UserRights.Any(x=>x==UserRightsCollection.Add.Id)) IsAdd = true;
                    if (response.UserRights.Any(x => x == UserRightsCollection.Delete.Id)) IsDelete = true;
                    if (response.UserRights.Any(x => x == UserRightsCollection.View.Id)) IsView = true;

                }
            }
        }
    }
}
