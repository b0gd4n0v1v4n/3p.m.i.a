using System;

namespace AimpLogic.UserRights
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException(string message)
            : base(message)
        {

        }
    }
}
