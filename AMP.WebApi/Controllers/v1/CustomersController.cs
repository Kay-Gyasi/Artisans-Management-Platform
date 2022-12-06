namespace AMP.WebApi.Controllers.v1;

[Authorize]
public class CustomersController : BaseControllerv1
{

    /// <summary>
    /// Returns a page of all active customers (For use by administrators only)
    /// </summary>
    /// <response code="200">Operation completed successfully</response>
    /// <response code="403">You do not have permission to access this resource</response>
    [Authorize(Roles = "Administrator, Developer")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<PaginatedList<CustomerPageDto>> GetPage([FromBody] PaginatedCommand command)
        => await Mediator.Send(new GetCustomerPage.Query(command));

    /// <summary>
    /// Returns info about a customer
    /// </summary>
    /// <response code="200">Request execution was successful and appropriate response has been returned</response>
    /// <response code="404">Request execution was successful but no data was found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<CustomerDto> Get(string id)
        => await Mediator.Send(new GetCustomer.Query(id));

    /// <summary>
    /// Returns info about a customer
    /// </summary>
    /// <response code="200">Request execution was successful and appropriate response has been returned</response>
    /// <response code="404">Request execution was successful but no data was found</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<CustomerDto> GetByUser()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return await Mediator.Send(new GetCustomerByUser.Query(userId));
    }

    /// <summary>
    /// Adds or updates info about a customer
    /// </summary>
    /// <response code="201">Artisan has been created successfully</response>
    /// <response code="403">You do not have permission to access this resource</response>
    [Authorize(Roles = "Administrator, Developer")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Save(CustomerCommand command)
    {
        var id = await Mediator.Send(new SaveCustomer.Command(command));
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    /// <summary>
    /// Removes a customer from the system
    /// </summary>
    /// <response code="204">Artisan has been deleted successfully</response>
    /// <response code="404">Artisan with id provided does not exist</response>
    /// <response code="403">You do not have permission to access this resource</response>
    [Authorize(Roles = "Customer, Administrator, Developer")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Delete(string id)
    {
        await Mediator.Send(new DeleteCustomer.Command(id));
        return NoContent();
    }
}