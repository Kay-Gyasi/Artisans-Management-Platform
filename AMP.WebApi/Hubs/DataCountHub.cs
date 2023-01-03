using AMP.Application.Features.Hubs;
using AMP.Processors.Hubs.Enums;
using Microsoft.AspNetCore.SignalR;

namespace AMP.WebApi.Hubs;

public class DataCountHub : Hub
{
    private readonly IMediator _mediator;

    public DataCountHub(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task Send(DataCountType type, string connectionId, string userId)
    {
        await Clients.Client(connectionId)
            .SendAsync("ReceiveCount",
                new CountMessage(type, await _mediator.Send(new GetCount.Command(type, userId))));
    }
}

public record CountMessage(DataCountType Type, int Value);