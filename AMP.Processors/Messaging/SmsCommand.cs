namespace AMP.Processors.Messaging;

public class SmsCommand
{
    public SmsCommand() { }
    
    public string Sender { get; set; } = "Tukofix";
    public string Message { get; set; }
    public IEnumerable<string> Recipients { get; set; }

    public static SmsCommand Create() => new();

    public SmsCommand WithMessage(string message)
    {
        Message = message;
        return this;
    }

    public SmsCommand To(IEnumerable<string> recipients)
    {
        Recipients = recipients;
        return this;
    }
}