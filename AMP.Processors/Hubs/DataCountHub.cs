using AMP.Processors.Processors.BusinessManagement;
using AMP.Processors.Processors.Messaging;
using Microsoft.AspNetCore.SignalR;

namespace AMP.Processors.Hubs;

[Authorize]
public class DataCountHub : Hub
{
    private readonly OrderProcessor _orderProcessor;
    private readonly PaymentProcessor _paymentProcessor;
    private readonly LookupProcessor _lookupProcessor;
    private readonly ChatMessageProcessor _chatMessageProcessor;
    private readonly ConnectRequestProcessor _connectRequestProcessor;

    private string UserId => Context.User?
        .FindFirst(ClaimTypes.NameIdentifier)?.Value;
    private string ConnectionId => Context.ConnectionId;

    public DataCountHub(OrderProcessor orderProcessor, 
        PaymentProcessor paymentProcessor,
        LookupProcessor lookupProcessor, ChatMessageProcessor chatMessageProcessor,
        ConnectRequestProcessor connectRequestProcessor)
    {
        _orderProcessor = orderProcessor;
        _paymentProcessor = paymentProcessor;
        _lookupProcessor = lookupProcessor;
        _chatMessageProcessor = chatMessageProcessor;
        _connectRequestProcessor = connectRequestProcessor;
    }

    public async Task Send(DataCountType type, string connectionId, string userId)
    {
        if (string.IsNullOrEmpty(connectionId)) connectionId = ConnectionId;
        if (string.IsNullOrEmpty(userId)) userId = UserId;

        if (!Connections.DataCount.ContainsKey(userId)) return;
        await Clients.Client(connectionId)
            .SendAsync(ClientMethods.ReceiveCount,
                new CountMessage(type, await GetCount(type, userId)));
    }

    public async Task GetUsers(string term, string type)
    {
        await Clients.Client(ConnectionId).SendAsync(ClientMethods.ReceiveUsersLookup,
            await _lookupProcessor.GetUsersLookup(term, type));
    }
    
    public override Task OnConnectedAsync()
    {
        Connections.DataCount.Remove(UserId);
        Connections.DataCount.Add(UserId, ConnectionId);
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        Connections.DataCount.Remove(UserId);
        return base.OnDisconnectedAsync(exception);
    }

    private async Task<int> GetCount(DataCountType type, string userId)
    {
        return type switch
        {
            DataCountType.Schedule => await _orderProcessor.GetScheduleCount(userId),
            DataCountType.JobRequests => await _orderProcessor.GetJobRequestsCount(userId),
            DataCountType.Payments => await _paymentProcessor.GetArtisanPaymentsCount(userId),
            DataCountType.Chats => await _chatMessageProcessor.GetUnreadMessages(userId),
            DataCountType.ConnectRequests => await _connectRequestProcessor.GetConnectRequestsCount(userId),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}