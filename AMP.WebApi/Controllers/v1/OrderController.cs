﻿namespace AMP.WebApi.Controllers.v1;

[Authorize]
public class OrderController : BaseControllerv1
{
    private string UserId => HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PaginatedList<OrderPageDto>> GetPage(PaginatedCommand command)
        => await Mediator.Send(new GetOrderPage.Query(command));
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PaginatedList<OrderPageDto>> GetSchedulePage([FromBody] PaginatedCommand command)
        => await Mediator.Send(new GetSchedule.Query(command, UserId));
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PaginatedList<OrderPageDto>> GetRequestPage([FromBody] PaginatedCommand command)
        => await Mediator.Send(new GetRequests.Query(command, UserId));
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PaginatedList<OrderPageDto>> GetHistoryPage([FromBody] PaginatedCommand command)
        => await Mediator.Send(new GetWorkHistory.Query(command, UserId));
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PaginatedList<OrderPageDto>> GetOrderHistoryPage([FromBody] PaginatedCommand command)
        => await Mediator.Send(new GetOrderHistory.Query(command, UserId));
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PaginatedList<OrderPageDto>> GetCustomerOrderPage([FromBody] PaginatedCommand command)
        => await Mediator.Send(new GetCustomerOrderPage.Query(command, UserId));

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<OrderDto> Get(string id)
        => await Mediator.Send(new GetOrder.Query(id));

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<InsertOrderResponse> Insert([FromBody] OrderCommand command) 
        => await Mediator.Send(new InsertOrder.Command(command));

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Save(OrderCommand command)
    {
        var id = await Mediator.Send(new SaveOrder.Command(command));
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Delete(string id)
        => await Mediator.Send(new DeleteOrder.Command(id));
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task UnassignArtisan(string id)
        => await Mediator.Send(new UnassignArtisan.Command(id));
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Complete(string id)
        => await Mediator.Send(new CompleteOrder.Command(id));
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task SetCost([FromBody] SetCostCommand command)
        => await Mediator.Send(new OrderCost.Command(command));
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task ArtisanComplete(string id)
        => await Mediator.Send(new ArtisanCompleteOrder.Command(id));
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Accept(string id)
        => await Mediator.Send(new AcceptOrder.Command(id));
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Cancel(string id)
        => await Mediator.Send(new CancelOrder.Command(id));
    
    [HttpPut("{artisanId}/{orderId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task AssignArtisan(string orderId, string artisanId)
        => await Mediator.Send(new AssignArtisan.Command(orderId, artisanId));
}