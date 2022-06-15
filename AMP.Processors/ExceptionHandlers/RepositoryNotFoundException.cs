using System;

namespace AMP.Processors.ExceptionHandlers
{
    public class RepositoryNotFoundException : Exception
    {
        public RepositoryNotFoundException(string message) : base(message)
        {
        }

        public RepositoryNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public RepositoryNotFoundException()
        {
        }
    }
}