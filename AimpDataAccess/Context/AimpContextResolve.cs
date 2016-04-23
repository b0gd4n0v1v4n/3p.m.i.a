using AimpDataAccess.EF;

namespace AimpDataAccess.Context
{
    public class AimpContextResolve
    {
        public static IAimpContext Context
        {
            get
            {
                return new EfAimpContext();
            }
        }
    }
}
