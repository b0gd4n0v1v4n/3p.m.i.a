using System;

namespace AimpLogic.Logging
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

        public void Log(string action,Exception ex)
        {

        }
        public void Log(Exception ex)
        {

        }
    }
}
