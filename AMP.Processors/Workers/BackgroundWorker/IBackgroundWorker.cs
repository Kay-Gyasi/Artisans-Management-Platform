namespace AMP.Processors.Workers.BackgroundWorker;

public interface IBackgroundWorker
{
    void SendSms(SmsType type, params object[] credentials);
    void ServeLiveCount(DataCountType type, string userId);
}

public class BackgroundWorker : IBackgroundWorker
{
    public void SendSms(SmsType type, params object[] credentials)
    {
        SmsService.DoTask(type, credentials);
    }

    public void ServeLiveCount(DataCountType type, string userId)
    {
        HubService.DoCountHubTask(type, userId);
    }
}