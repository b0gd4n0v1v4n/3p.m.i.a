using System;
using log4net;
using log4net.Config;

namespace Aimp.Domain
{
    public class Logger : ILogger
    {
        private ILog _log;
        public Logger()
        {
            _log = LogManager.GetLogger(typeof(Logger));
            XmlConfigurator.Configure();
        }

        public void Log(string message)
        {
            _log.Info(message);
            if (LogEvent != null)
            {
                try
                {
                    LogEvent(message);
                }
                catch
                {
                    
                }
            }
        }

        public void Log(Exception ex)
        {
            _log.Error(ex);

            if (LogEvent != null)
            {
                try
                {
                    Exception exception = ex;
                    while (exception != null)
                    {
                        _log.Error(ex.Message, ex);
                        _log.Error(ex.StackTrace);
                        LogEvent(ex.Message);
                        LogEvent(ex.StackTrace);
                        exception = exception.InnerException;
                    }
                }
                catch
                {
                }
            }
        }

        public event Action<string> LogEvent;
    }
}