using System;

namespace Aimp.Domain
{
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException(string message) 
            : base(message)
        {

        }
    }
}
