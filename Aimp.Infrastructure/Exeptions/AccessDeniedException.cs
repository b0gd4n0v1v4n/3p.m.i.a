using System;

namespace Aimp.Infrastructure.Users.Exeptions
{
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException(string message) 
            : base(message)
        {

        }
    }
}
