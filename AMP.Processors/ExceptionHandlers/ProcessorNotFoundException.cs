using System;

namespace AMP.Processors.ExceptionHandlers
{
    public class ProcessorNotFoundException : Exception
    {
        public ProcessorNotFoundException(string message) : base(message)
        {
        }

        public ProcessorNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ProcessorNotFoundException()
        {
        }
    }
}