using Aimp.Infrastructure.Logging;

namespace Aimp.Wcf
{
    public class LoggerProvider
    {
        private static object _sync = new object();
        private static ILogger _instance;
        public static ILogger Logger { get
            {
                //lock(_sync)
                //    if(_instance == null)
                //        _instance = new 
                return _instance;
            }
        }
    }
}
