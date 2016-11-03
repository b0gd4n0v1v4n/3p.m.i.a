using System;

namespace Aimp.Domain
{
    public interface ILogger
    {
        void Log(string message);
        void Log(Exception ex);
        event Action<string> LogEvent;
    }
}
