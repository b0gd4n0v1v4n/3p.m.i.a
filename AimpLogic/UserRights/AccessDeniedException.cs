using System;

namespace AimpLogic.UserRights
{
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException(string message) 
            : base(message)
        {

        }
    }
}
