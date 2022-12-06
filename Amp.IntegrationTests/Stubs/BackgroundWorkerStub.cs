using AMP.Processors.Messaging;
using AMP.Processors.Workers.BackgroundWorker;
using AMP.Processors.Workers.Enums;

namespace Amp.IntegrationTests.Stubs;

public class BackgroundWorkerStub : IBackgroundWorker
{
    public void SendSms(SmsType type, params object[] credentials)
    {
        Console.WriteLine("Sms sent");
    }
}