using AMP.Processors.Workers.Enums;

namespace AMP.Processors.Workers.BackgroundWorker;

public interface IBackgroundWorker
{
    void SendSms(SmsType type, params object[] credentials);
}

public class BackgroundWorker : IBackgroundWorker
{
    public void SendSms(SmsType type, params object[] credentials)
    {
        SmsService.DoTask(type, credentials);
    }
}