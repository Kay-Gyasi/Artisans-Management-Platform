namespace AMP.Processors.Exceptions
{
    public class InvalidEntityException : Exception
    {
        public InvalidEntityException(string message) : base(message)
        {
        }

        public InvalidEntityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidEntityException(){}
    }
}