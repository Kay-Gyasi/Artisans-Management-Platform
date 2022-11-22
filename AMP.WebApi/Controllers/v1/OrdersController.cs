namespace AMP.WebApi.Controllers.v1;

[Authorize]
public class OrdersController : BaseControllerv1
{
    private string UserId => HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    
    /// <summary>
    /// Returns a page of all orders placed in the system (For use by administrators only)
    /// </summary>
    [Authorize(Roles = "Administrator")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PaginatedList<OrderPageDto>> GetPage(PaginatedCommand command)
        => await Mediator.Send(new GetOrderPage.Query(command));
    
    /// <summary>
    /// Returns a page of orders scheduled to be worked on for requesting artisan
    /// </summary>
    /// <response code="200">Request execution was successful and appropriate response has been returned</response>
    /// <response code="404">Request execution was successful but no data was found</response>
    /// <response code="500">A server or command validation error has occurred</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<PaginatedList<OrderPageDto>> GetSchedulePage([FromBody] PaginatedCommand command)
        => await Mediator.Send(new GetSchedule.Query(command, UserId));
    
    /// <summary>
    /// Returns a page of order requests made to requesting artisan
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PaginatedList<OrderPageDto>> GetRequestPage([FromBody] PaginatedCommand command)
        => await Mediator.Send(new GetRequests.Query(command, UserId));
    
    /// <summary>
    /// Returns a page of completed orders worked on by requesting artisan (For use by artisans only)
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PaginatedList<OrderPageDto>> GetHistoryPage([FromBody] PaginatedCommand command)
        => await Mediator.Send(new GetWorkHistory.Query(command, UserId));
    
    /// <summary>
    /// Returns a page of completed orders which were placed by requesting customer (For use by customers only)
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PaginatedList<OrderPageDto>> GetOrderHistoryPage([FromBody] PaginatedCommand command)
        => await Mediator.Send(new GetOrderHistory.Query(command, UserId));
    
    /// <summary>
    /// Returns a page of active orders which were placed by requesting customer
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PaginatedList<OrderPageDto>> GetCustomerOrderPage([FromBody] PaginatedCommand command)
        => await Mediator.Send(new GetCustomerOrderPage.Query(command, UserId));

    /// <summary>
    /// Returns details about an order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<OrderDto> Get(string id)
        => await Mediator.Send(new GetOrder.Query(id));

    /// <summary>
    /// Places a new order on behalf of customer
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<InsertOrderResponse> Insert([FromBody] OrderCommand command) 
        => await Mediator.Send(new InsertOrder.Command(command));

    /// <summary>
    /// Updates an order's info
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Save(OrderCommand command)
    {
        var id = await Mediator.Send(new SaveOrder.Command(command));
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    /// <summary>
    /// Deletes an order from the system
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Delete(string id)
        => await Mediator.Send(new DeleteOrder.Command(id));
    
    /// <summary>
    /// Unassigns an artisan from an active order
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task UnassignArtisan(string id)
        => await Mediator.Send(new UnassignArtisan.Command(id));
    
    /// <summary>
    /// Completes an order (For use by customers only)
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Complete(string id)
        => await Mediator.Send(new CompleteOrder.Command(id));
    
    /// <summary>
    /// Sets cost on an order (For use by artisans only)
    /// </summary>
    [Authorize(Roles = "Artisan")]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task SetCost([FromBody] SetCostCommand command)
        => await Mediator.Send(new OrderCost.Command(command));
    
    /// <summary>
    /// Marks an order as completed by the assigned artisan
    /// </summary>
    [Authorize(Roles = "Artisan")]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task ArtisanComplete(string id)
        => await Mediator.Send(new ArtisanCompleteOrder.Command(id));
    
    /// <summary>
    /// Adds an order to an artisan's schedule
    /// </summary>
    [Authorize(Roles = "Artisan")]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Accept(string id)
        => await Mediator.Send(new AcceptOrder.Command(id));
    
    /// <summary>
    /// Removes assigned artisan and resets order
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Cancel(string id)
        => await Mediator.Send(new CancelOrder.Command(id));
    
    /// <summary>
    /// Assigns an artisan to an order
    /// </summary>
    [HttpPut("{artisanId}/{orderId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task AssignArtisan(string orderId, string artisanId)
        => await Mediator.Send(new AssignArtisan.Command(orderId, artisanId));
}