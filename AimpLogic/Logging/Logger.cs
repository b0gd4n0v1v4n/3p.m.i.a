using log4net;
using log4net.Config;
using System;

namespace AimpLogic.Logging
{
    public class Logger
    {
        private static Logger _logger;
        private ILog _log;
        private Logger()
        {
            _log = LogManager.GetLogger(typeof(Logger));
            XmlConfigurator.Configure();
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
            _log.Debug(ex);
        }
    }
}
