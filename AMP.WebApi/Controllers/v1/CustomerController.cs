namespace AMP.WebApi.Controllers.v1;

[Authorize]
public class CustomerController : BaseControllerv1
{

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PaginatedList<CustomerPageDto>> GetPage([FromBody] PaginatedCommand command)
        => await Mediator.Send(new GetCustomerPage.Query(command));

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<CustomerDto> Get(string id)
        => await Mediator.Send(new GetCustomer.Query(id));

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<CustomerDto> GetByUser()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return await Mediator.Send(new GetCustomerByUser.Query(userId));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Save(CustomerCommand command)
    {
        var id = await Mediator.Send(new SaveCustomer.Command(command));
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Delete(string id)
        => await Mediator.Send(new DeleteCustomer.Command(id));
}