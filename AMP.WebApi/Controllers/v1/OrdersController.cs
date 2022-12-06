﻿namespace AMP.WebApi.Controllers.v1;

[Authorize]
public class OrdersController : BaseControllerv1
{
    
    /// <summary>
    /// Returns a page of all orders placed in the system (For use by administrators only)
    /// </summary>
    /// <response code="200">Operation completed successfully</response>
    /// <response code="404">Operation failed</response>
    /// <response code="403">You do not have permission to access this resource</response>
    [Authorize(Roles = "Administrator, Developer")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<PaginatedList<OrderPageDto>> GetPage(PaginatedCommand command)
        => await Mediator.Send(new GetOrderPage.Query(command));
    
    /// <summary>
    /// Returns a page of orders scheduled to be worked on for requesting artisan
    /// </summary>
    /// <response code="200">Request execution was successful and appropriate response has been returned</response>
    /// <response code="404">Request execution was successful but no data was found</response>
    /// <response code="403">You do not have permission to access this resource</response>
    [Authorize(Roles = "Artisan, Administrator, Developer")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<PaginatedList<OrderPageDto>> GetSchedulePage([FromBody] PaginatedCommand command)
        => await Mediator.Send(new GetSchedule.Query(command, UserId));
    
    /// <summary>
    /// Returns a page of order requests made to requesting artisan
    /// </summary>
    /// <response code="200">Request execution was successful and appropriate response has been returned</response>
    /// <response code="404">Request execution was successful but no data was found</response>
    /// <response code="403">You do not have permission to access this resource</response>
    [Authorize(Roles = "Artisan, Administrator, Developer")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<PaginatedList<OrderPageDto>> GetRequestPage([FromBody] PaginatedCommand command)
        => await Mediator.Send(new GetRequests.Query(command, UserId));
    
    /// <summary>
    /// Returns a page of completed orders worked on by requesting artisan
    /// </summary>
    /// <response code="200">Request execution was successful and appropriate response has been returned</response>
    /// <response code="404">Request execution was successful but no data was found</response>
    /// <response code="403">You do not have permission to access this resource</response>
    [Authorize(Roles = "Artisan, Administrator, Developer")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<PaginatedList<OrderPageDto>> GetHistoryPage([FromBody] PaginatedCommand command)
        => await Mediator.Send(new GetWorkHistory.Query(command, UserId));
    
    /// <summary>
    /// Returns a page of completed orders which were placed by requesting customer
    /// </summary>
    /// <response code="200">Request execution was successful and appropriate response has been returned</response>
    /// <response code="404">Request execution was successful but no data was found</response>
    /// <response code="403">You do not have permission to access this resource</response>
    [Authorize(Roles = "Customer, Administrator, Developer")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<PaginatedList<OrderPageDto>> GetOrderHistoryPage([FromBody] PaginatedCommand command)
        => await Mediator.Send(new GetOrderHistory.Query(command, UserId));
    
    /// <summary>
    /// Returns a page of active orders which were placed by requesting customer
    /// </summary>
    /// <response code="200">Request execution was successful and appropriate response has been returned</response>
    /// <response code="404">Request execution was successful but no data was found</response>
    /// <response code="403">You do not have permission to access this resource</response>
    [Authorize(Roles = "Customer, Administrator, Developer")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<PaginatedList<OrderPageDto>> GetCustomerOrderPage([FromBody] PaginatedCommand command)
    {
        return await Mediator.Send(new GetCustomerOrderPage.Query(command, UserId));
    }

    /// <summary>
    /// Returns details about an order
    /// </summary>
    /// <response code="200">Request execution was successful and appropriate response has been returned</response>
    /// <response code="404">Request execution was successful but no data was found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<OrderDto> Get(string id)
        => await Mediator.Send(new GetOrder.Query(id));

    /// <summary>
    /// Places a new order on behalf of customer
    /// </summary>
    /// <response code="200">Order has been placed successfully</response>
    /// <response code="404">Order placement failed</response>
    /// <response code="403">You do not have permission to access this resource</response>
    /// <response code="412">Input is missing some required fields</response>
    [Authorize(Roles = "Customer, Administrator, Developer")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
    public async Task<InsertOrderResponse> Insert([FromBody] OrderCommand command) 
        => await Mediator.Send(new InsertOrder.Command(command));

    /// <summary>
    /// Updates an order's info
    /// </summary>
    /// <response code="201">Order has been updated successfully</response>
    /// <response code="404">Order update failed</response>
    /// <response code="403">You do not have permission to access this resource</response>
    /// <response code="412">Input is missing some required fields</response>
    [Authorize(Roles = "Customer, Administrator, Developer")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
    public async Task<IActionResult> Save(OrderCommand command)
    {
        var id = await Mediator.Send(new SaveOrder.Command(command));
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    /// <summary>
    /// Deletes an order from the system
    /// </summary>
    /// <response code="204">Order has been deleted successfully</response>
    /// <response code="404">Order with id provided does not exist</response>
    /// <response code="403">You do not have permission to delete the order</response>
    [Authorize(Roles = "Customer, Administrator, Developer")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Delete(string id)
    {
        await Mediator.Send(new DeleteOrder.Command(id));
        return NoContent();
    }

    /// <summary>
    /// Unassigns an artisan from an active order
    /// </summary>
    /// <response code="204">Artisan has been unassigned successfully</response>
    /// <response code="404">Order with id provided does not exist</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UnassignArtisan(string id)
    {
        await Mediator.Send(new UnassignArtisan.Command(id));
        return NoContent();
    }

    /// <summary>
    /// Completes an order (For use by customers only)
    /// </summary>
    /// <response code="204">Order has been completed successfully</response>
    /// <response code="404">Order with id provided does not exist</response>
    /// <response code="403">You do not have permission to access this resource</response>
    [Authorize(Roles = "Customer, Administrator, Developer")]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Complete(string id)
    {
        await Mediator.Send(new CompleteOrder.Command(id));
        return NoContent();
    }

    /// <summary>
    /// Sets cost on an order (For use by artisans only)
    /// </summary>
    /// <response code="204">Cost has been set on order successfully</response>
    /// <response code="404">Order with id provided does not exist</response>
    /// <response code="403">You do not have permission to access this resource</response>
    [Authorize(Roles = "Artisan, Administrator, Developer")]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> SetCost([FromBody] SetCostCommand command)
    {
        await Mediator.Send(new OrderCost.Command(command));
        return NoContent();
    }

    /// <summary>
    /// Marks an order as completed by the assigned artisan
    /// </summary>
    /// <response code="204">Artisan has completed work on order successfully</response>
    /// <response code="404">Order with id provided does not exist</response>
    /// <response code="403">You do not have permission to access this resource</response>
    [Authorize(Roles = "Artisan, Administrator, Developer")]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> ArtisanComplete(string id)
    {
        await Mediator.Send(new ArtisanCompleteOrder.Command(id));
        return NoContent();
    }

    /// <summary>
    /// Adds an order to an artisan's schedule
    /// </summary>
    /// <response code="204">Order has been accepted by artisan</response>
    /// <response code="404">Order with id provided does not exist</response>
    /// <response code="403">You do not have permission to access this resource</response>
    [Authorize(Roles = "Artisan, Administrator, Developer")]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Accept(string id)
    {
        await Mediator.Send(new AcceptOrder.Command(id));
        return NoContent();
    }

    /// <summary>
    /// Declines order request or removes order acceptance state
    /// </summary>
    /// <response code="204">Order has been declined by artisan</response>
    /// <response code="404">Order with id provided does not exist</response>
    /// <response code="403">You do not have permission to access this resource</response>
    [Authorize(Roles = "Artisan, Administrator, Developer")]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]    
    public async Task<IActionResult> Cancel(string id)
    {
        await Mediator.Send(new CancelOrder.Command(id));
        return NoContent();
    }

    /// <summary>
    /// Assigns an artisan to an order
    /// </summary>
    /// <response code="204">Artisan has been unassigned successfully</response>
    /// <response code="404">Order with id provided does not exist</response>
    /// <response code="403">You do not have permission to access this resource</response>
    [Authorize(Roles = "Customer, Administrator, Developer")]
    [HttpPut("{artisanId}/{orderId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> AssignArtisan(string orderId, string artisanId)
    {
        await Mediator.Send(new AssignArtisan.Command(orderId, artisanId));
        return NoContent();
    }
}