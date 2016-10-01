using AimpDataAccess.Context;

namespace AimpLogic.DataContext
{
    public class AimpContextResolver
    {
        private AimpContextResolver()
        {

        }
        private static object _syncInstance = new object();
        private static IAimpContext _context;
        public static IAimpContext Instance
        {
            get
            {
                lock (_syncInstance)
                {
                    if(_context == null)
                    {
                        _context = AimpContextResolve.Context;
                    }
                    return _context;
                }
            }
        }
    }
}
