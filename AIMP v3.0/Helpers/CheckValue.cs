using System;

namespace AIMP_v3._0.Helpers
{
    public static class CheckValue
    {
        public static bool Check(object obj)
        {
            if (obj == null)
                return false;

            if (obj is DateTime)
            {
                if (((DateTime)obj) <= DateTime.MinValue)
                    return false;
                if (((DateTime)obj) > DateTime.MaxValue)
                    return false;
            }

            if (string.IsNullOrWhiteSpace(obj.ToString()))
                return false;

            return true;
        }
    }
}
