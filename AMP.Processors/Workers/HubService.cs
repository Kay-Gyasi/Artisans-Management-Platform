using AMP.Processors.Processors.BusinessManagement;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AMP.Processors.Workers;

public class HubService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<HubService> _logger;
    private readonly IHubContext<DataCountHub> _countHubContext;

    private static List<OutgoingCountHubEvents> _events = new();
    
    public HubService(IServiceProvider serviceProvider, ILogger<HubService> logger,
        IHubContext<DataCountHub> countHubContext)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _countHubContext = countHubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
            if (_events.Count == 0) continue;
            
            var eventUnderConsideration = _events.First();
            await _countHubContext.Clients.Client(Connections.DataCount[eventUnderConsideration.UserId])
                .SendAsync(ClientMethods.ReceiveCount, 
                    new CountMessage(eventUnderConsideration.Type, 
                        await GetCount(eventUnderConsideration.Type, eventUnderConsideration.UserId))
                    , stoppingToken);
            _events.Remove(eventUnderConsideration);
        }
    }

    public static void DoCountHubTask(DataCountType type, string userId)
    {
        _events.Add(new OutgoingCountHubEvents(type, userId));
    }
    
    private async Task<int> GetCount(DataCountType type, string userId)
    {
        using var scope = _serviceProvider.CreateScope();
        var orderProcessor = scope.ServiceProvider.GetRequiredService<OrderProcessor>();
        var paymentProcessor = scope.ServiceProvider.GetRequiredService<PaymentProcessor>();
        return type switch
        {
            DataCountType.Schedule => await orderProcessor.GetScheduleCount(userId),
            DataCountType.JobRequests => await orderProcessor.GetJobRequestsCount(userId),
            DataCountType.Payments => await paymentProcessor.GetArtisanPaymentsCount(userId),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

public class OutgoingCountHubEvents
{
    public OutgoingCountHubEvents(DataCountType type, string userId)
    {
        Type = type;
        UserId = userId;
    }
    
    public DataCountType Type { get; }
    public string UserId { get; }
}