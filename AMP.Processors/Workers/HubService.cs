using AMP.Processors.Processors.BusinessManagement;
using AMP.Processors.Processors.Messaging;
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

            try
            {
                foreach (var eventUnderConsideration in _events)
                {
                    if (!Connections.DataCount.ContainsKey(eventUnderConsideration.UserId)) continue;

                    if (eventUnderConsideration.Type == DataCountType.RefreshChat)
                    {
                        await _countHubContext.Clients.Client(Connections.DataCount[eventUnderConsideration.UserId])
                            .SendAsync(ClientMethods.RefreshChat, 
                                new CountMessage(eventUnderConsideration.Type, 
                                    0, eventUnderConsideration.ConversationId)
                                , stoppingToken);
                        _events.Remove(eventUnderConsideration);
                        continue;
                    }
                
                    await _countHubContext.Clients.Client(Connections.DataCount[eventUnderConsideration.UserId])
                        .SendAsync(ClientMethods.ReceiveCount, 
                            new CountMessage(eventUnderConsideration.Type, 
                                await GetCount(eventUnderConsideration.Type, eventUnderConsideration.UserId))
                            , stoppingToken);
                    _events.Remove(eventUnderConsideration);
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Hub service request failed");
            }
        }
    }

    public static void DoCountHubTask(DataCountType type, string userId, string conversationId = null)
    {
        _events.Add(new OutgoingCountHubEvents(type, userId, conversationId));
    }
    
    private async Task<int> GetCount(DataCountType type, string userId)
    {
        using var scope = _serviceProvider.CreateScope();
        var orderProcessor = scope.ServiceProvider.GetRequiredService<OrderProcessor>();
        var paymentProcessor = scope.ServiceProvider.GetRequiredService<PaymentProcessor>();
        var chatMessageProcessor = scope.ServiceProvider.GetRequiredService<ChatMessageProcessor>();
        var connectRequestProcessor = scope.ServiceProvider.GetRequiredService<ConnectRequestProcessor>();
        return type switch
        {
            DataCountType.Schedule => await orderProcessor.GetScheduleCount(userId),
            DataCountType.JobRequests => await orderProcessor.GetJobRequestsCount(userId),
            DataCountType.Payments => await paymentProcessor.GetArtisanPaymentsCount(userId),
            DataCountType.Chats => await chatMessageProcessor.GetUnreadMessages(userId),
            DataCountType.ConnectRequests => await connectRequestProcessor.GetConnectRequestsCount(userId),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

public class OutgoingCountHubEvents
{
    public OutgoingCountHubEvents(DataCountType type, string userId, string conversationId = null)
    {
        Type = type;
        UserId = userId;
        ConversationId = conversationId;
    }
    
    public DataCountType Type { get; }
    public string UserId { get; }
    public string ConversationId { get; }
}