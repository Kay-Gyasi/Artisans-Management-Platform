using System.Collections.Generic;
using System.Linq;

namespace AMP.Processors.Messaging;

public class SmsCommand
{
    public SmsCommand() { }
    
    public string Sender { get; set; } = "Qface Group";
    public string Message { get; set; }
    public IEnumerable<string> Recipients { get; set; }

    public static SmsCommand Create() => new SmsCommand();

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