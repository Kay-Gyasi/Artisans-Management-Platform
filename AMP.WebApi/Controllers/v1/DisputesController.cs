namespace AMP.WebApi.Controllers.v1;

[Authorize(Roles = "Customer")]
public class DisputesController : BaseControllerv1
{

    /// <summary>
    /// Returns a page of disputes filed by requesting customer
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PaginatedList<DisputePageDto>> GetPage(PaginatedCommand command)
        => await Mediator.Send(new GetDisputePage.Query(command, UserId));

    /// <summary>
    /// Returns details of a filed dispute
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<DisputeDto> Get(string id)
        => await Mediator.Send(new GetDispute.Query(id));
    
    /// <summary>
    /// Returns info on all open disputes
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<DisputeCount> GetOpenDisputeCount()
        => await Mediator.Send(new GetOpenDisputeCount.Query(UserId));

    /// <summary>
    /// Adds or updates a dispute on an order
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Save(DisputeCommand command)
    {
        var id = await Mediator.Send(new SaveDispute.Command(command, UserId));
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    /// <summary>
    /// Deletes a dispute from the system
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Delete(string id)
        => await Mediator.Send(new DeleteDispute.Command(id));
}