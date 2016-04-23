using AIMP_v3._0.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AIMP_v3._0.ViewModel
{
    public class AuthorizationViewModel : BaseViewModel
    {
        public void Auth(string login,string password)
        {
            try
            {
                CurrentUser.Auth(login, password);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
