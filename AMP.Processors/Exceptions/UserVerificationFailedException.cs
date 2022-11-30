namespace AMP.Processors.Exceptions;

public class UserVerificationFailedException : Exception
{
    public UserVerificationFailedException() { }
    public UserVerificationFailedException(string message) : base(message)
    { }

}