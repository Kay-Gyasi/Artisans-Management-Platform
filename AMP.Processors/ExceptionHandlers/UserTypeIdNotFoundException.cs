using System;

namespace AMP.Processors.ExceptionHandlers
{
    public class UserTypeIdNotFoundException : Exception
    {
        public UserTypeIdNotFoundException(string message) : base(message)
        {
        }

        public UserTypeIdNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public UserTypeIdNotFoundException()
        {
        }
    }
}