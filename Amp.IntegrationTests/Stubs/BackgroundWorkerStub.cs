using AMP.Processors.Dtos.Messaging;
using AMP.Processors.Hubs.Enums;
using AMP.Processors.Workers.BackgroundWorker;
using AMP.Processors.Workers.Enums;

namespace Amp.IntegrationTests.Stubs;

public class BackgroundWorkerStub : IBackgroundWorker
{
    public void SendSms(SmsType type, params object[] credentials)
    {
        Console.WriteLine("Sms sent");
    }

    public void ServeHub(DataCountType type, string userId, string conversationId = null)
    {
        Console.WriteLine("Served");
    }
}