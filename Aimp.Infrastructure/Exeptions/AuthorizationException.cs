using System;

namespace Aimp.Infrastructure.Users.Exeptions
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException(string message)
            : base(message)
        {

        }
    }
}
