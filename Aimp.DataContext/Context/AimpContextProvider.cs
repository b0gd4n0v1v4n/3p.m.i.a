using Aimp.DataContext.EF;

namespace Aimp.DataContext.Context
{
    public class AimpContextProvider
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
