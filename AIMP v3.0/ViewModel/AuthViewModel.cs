using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMP_v3._0.ViewModel
{
    public class AuthViewModel
    {
        public string ServiceName { get; set; }
        public string Address { get; set; }
        public string Port { get; set; }
        public string Login { get; set; }
public string Password { get; set; }
        public bool IsSavePassword { get; set; }
        public Command ConnectCommand { get { return new Command((win) => { }); } }
        public Command ExitCommand { get { return new Command((win) => { }); } }

    }
}
