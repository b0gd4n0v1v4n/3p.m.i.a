using Aimp.DataContext.Context;
using Aimp.DataContext;

namespace Aimp.Logic
{
    public static class DataContextProvider
    {
        private static object _sync = new object();
        private static IAimpContext _instance;
        public static IAimpContext Context
        {
            get
            {
                lock (_sync)
                    if (_instance == null)
                        _instance = AimpContextProvider.Context;

                return _instance;
            }
        }
    }
}
