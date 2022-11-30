namespace AMP.Processors.Exceptions;

public class SmsException : Exception
{
    public SmsException() { }
    public SmsException(string message) : base(message) { }
}