using AMP.Processors.Workers.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AMP.Processors.Workers;

public class SmsService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<SmsService> _logger;

    private static bool _startTask;
    private static SmsType _smsType;
    private static object[] _credentials;

    public SmsService(IServiceProvider serviceProvider, ILogger<SmsService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
            if (!_startTask) continue;
            try
            {
                await SendMessage();
            }
            catch (Exception e)
            {
                _logger.LogError("SMS send failed!");
            }
        }
    }

    private async Task SendMessage()
    {
        using var scope = _serviceProvider.CreateScope();
        var smsMessaging = scope.ServiceProvider.GetRequiredService<ISmsMessaging>();
        var messageGenerator = scope.ServiceProvider.GetRequiredService<MessageGenerator>();

        switch (_smsType)
        {
            case SmsType.AssignArtisan:
                await AssignArtisan(messageGenerator, smsMessaging);
                break;
            case SmsType.UnassignArtisan:
                await UnassignArtisan(messageGenerator, smsMessaging);
                break;
            case SmsType.AcceptRequest:
                await AcceptRequest(messageGenerator, smsMessaging);
                break;
            case SmsType.PaymentVerified:
                await CustomerPaymentVerified(messageGenerator, smsMessaging); 
                await ArtisanPaymentVerified(messageGenerator, smsMessaging);
                FinishTask();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public static void DoTask(SmsType type, params object[] credentials)
    {
        _smsType = type;
        _credentials = credentials;
        _startTask = true;
    }

    private static async Task AssignArtisan(MessageGenerator messageGenerator, ISmsMessaging smsMessaging)
    {
        var message = await messageGenerator.AssignArtisan(_credentials[0].ToString(),
            _credentials[1].ToString());
        var command = SmsCommand.Create()
            .WithMessage(message.Item1)
            .To(new[] {message.Item2});
        await smsMessaging.Send(command);
        FinishTask();
    } 
    
    private static async Task UnassignArtisan(MessageGenerator messageGenerator, ISmsMessaging smsMessaging)
    {
        var message = await messageGenerator.UnassignArtisan(_credentials[0].ToString());
        var command = SmsCommand.Create()
            .WithMessage(message.Item1)
            .To(new[] {message.Item2});
        await smsMessaging.Send(command);
        FinishTask();
    } 
    
    private static async Task AcceptRequest(MessageGenerator messageGenerator, ISmsMessaging smsMessaging)
    {
        var message = await messageGenerator.AcceptRequest(_credentials[0].ToString());
        var command = SmsCommand.Create()
            .WithMessage(message.Item1)
            .To(new[] {message.Item2});
        await smsMessaging.Send(command);
        FinishTask();
    }
    
    private static async Task CustomerPaymentVerified(MessageGenerator messageGenerator, ISmsMessaging smsMessaging)
    {
        var message = await messageGenerator.CustomerPaymentVerified(_credentials[0].ToString());
        var command = SmsCommand.Create()
            .WithMessage(message.Item1)
            .To(new[] {message.Item2});
        await smsMessaging.Send(command);
    }
    
    private static async Task ArtisanPaymentVerified(MessageGenerator messageGenerator, ISmsMessaging smsMessaging)
    {
        var message = await messageGenerator.ArtisanPaymentVerified(_credentials[0].ToString());
        var command = SmsCommand.Create()
            .WithMessage(message.Item1)
            .To(new[] {message.Item2});
        await smsMessaging.Send(command);
    }

    private static void FinishTask()
    {
        _smsType = default;
        _credentials = default;
        _startTask = false;
    }

    
}