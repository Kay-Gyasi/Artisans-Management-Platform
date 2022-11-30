namespace AMP.Processors.Exceptions
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