using System;
using System.Windows;

namespace AIMP_v3._0.Logging
{
    public class Logger
    {
        private static Logger _logger;

        private Logger()
        {

        }

        public static Logger Instance
        {
            get
            {
                if (_logger == null)
                    _logger = new Logger();

                return _logger;
            }
        }

        public void Log(string message,string action,Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}
