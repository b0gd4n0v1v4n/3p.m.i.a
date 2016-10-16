using AIMP_v3._0.DataAccess;
using AIMP_v3._0.Helpers;
using AIMP_v3._0.Properties;
using AIMP_v3._0.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace AIMP_v3._0.ViewModel
{
    public class AuthViewModel
    {
        public AuthViewModel()
        {
            try
            {
                var setings = Settings.Default;
                ServiceName = setings.service;
                Address = setings.hostname;
                Port = setings.port;
                Login = setings.login;
                Password = setings.password;
                IsSavePassword = setings.IsSave;
            }
            catch
            {

            }
        }
        public string ServiceName { get; set; }
        public string Address { get; set; }
        public string Port { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsSavePassword { get; set; }
        public Command ConnectCommand
        {
            get
            {
                return new Command((win) =>
{
    try
    {
        ConnectionSettings.Address = $"http://{Address}:{Port}/{ServiceName}";
        CurrentUser.Auth(Login, Password);

        var setings = Settings.Default;

        if (IsSavePassword)
        {
            setings.hostname = Address;
            setings.port = Port;
            setings.service = ServiceName;
            setings.login = Login;
            setings.password = Password;
            setings.IsSave = IsSavePassword;

        }
        else
        {
            setings.password = null;
            setings.IsSave = false;
        }
        setings.Save();

        ((Window)win)?.Hide();
        MainView mian = new MainView();
        mian.ShowDialog();
        ((Window)win)?.Show();
    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message);
    }
});
            }
        }
        public Command ExitCommand { get { return new Command((win) => { ((Window)win)?.Close(); }); } }

    }
}
