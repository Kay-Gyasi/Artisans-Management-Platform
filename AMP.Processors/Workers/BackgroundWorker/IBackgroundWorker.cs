using AMP.Processors.Dtos.Messaging;

namespace AMP.Processors.Workers.BackgroundWorker;

public interface IBackgroundWorker
{
    void SendSms(SmsType type, params object[] credentials);
    void ServeHub(DataCountType type, string userId, string conversationId = null);
}

public class BackgroundWorker : IBackgroundWorker
{
    public void SendSms(SmsType type, params object[] credentials)
    {
        SmsService.DoTask(type, credentials);
    }

    public void ServeHub(DataCountType type, string userId, string conversationId = null)
    {
        HubService.DoCountHubTask(type, userId, conversationId);
    }
}