namespace AMP.Processors.Messaging;

public interface ISmsMessaging
{
    Task Send(SmsCommand command);
}