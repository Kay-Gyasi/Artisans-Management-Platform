namespace AMP.Processors.Exceptions;

public class NullRegistrationException : Exception
{
    public NullRegistrationException(string message) : base(message)
    {
    }

    public NullRegistrationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public NullRegistrationException()
    {
    }
}