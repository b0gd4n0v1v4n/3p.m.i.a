using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMP_v3._0
{
    public static class CurrentUser
    {
        public static bool IsAdmin { get; }
        public static bool IsView { get; }
        public static bool IsDelete { get; }
        public static bool IsAdd { get; }
    }
}
