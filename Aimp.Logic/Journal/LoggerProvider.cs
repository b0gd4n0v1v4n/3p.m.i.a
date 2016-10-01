using Aimp.Infrastructure.Logging;

namespace Aimp.Logic.Journal
{
    public static class LoggerProvider
    {
        private static ILogger _logger;
        private static object _sync = new object();
        public static ILogger Logger
        {
            get
            {
                //lock(_sync)
                //    if (_logger == null)
                    //_logger = new Logger();

                return _logger;
            }
        }
    }
}
