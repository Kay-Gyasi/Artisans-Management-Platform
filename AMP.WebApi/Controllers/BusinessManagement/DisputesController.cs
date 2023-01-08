using AMP.Application.Features.Commands.BusinessManagement;
using AMP.Application.Features.Queries.BusinessManagement;
using AMP.Processors.Commands.BusinessManagement;
using AMP.Processors.PageDtos.BusinessManagement;

namespace AMP.WebApi.Controllers.v1;

[Authorize(Roles = "Customer, Administrator, Developer")]
public class DisputesController : BaseControllerv1
{

    /// <summary>
    /// Returns a page of disputes filed by requesting customer
    /// </summary>
    /// <response code="201">Dispute has been filed successfully</response>
    /// <response code="403">You do not have permission to access this resource</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<PaginatedList<DisputePageDto>> GetPage(PaginatedCommand command)
        => await Mediator.Send(new GetDisputePage.Query(command, UserId));

    /// <summary>
    /// Returns details of a filed dispute
    /// </summary>
    /// <response code="200">Request execution was successful and appropriate response has been returned</response>
    /// <response code="404">Request execution was successful but no data was found</response>
    /// <response code="403">You do not have permission to access this resource</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Get(string id)
    {
        var result = await Mediator.Send(new GetDispute.Query(id)).ConfigureAwait(false);
        return await OkResult(result);
    }

    /// <summary>
    /// Returns info on all open disputes
    /// </summary>
    /// <response code="200">Request execution was successful and appropriate response has been returned</response>
    /// <response code="403">You do not have permission to access this resource</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetOpenDisputeCount()
    {
        var result = await Mediator.Send(new GetOpenDisputeCount.Query(UserId)).ConfigureAwait(false);
        return await OkResult(result);
    }

    /// <summary>
    /// Adds or updates a dispute on an order
    /// </summary>
    /// <response code="200">Request execution was successful and appropriate response has been returned</response>
    /// <response code="404">Request execution was successful but no data was found</response>
    /// <response code="403">You do not have permission to access this resource</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Save(DisputeCommand command)
    {
        var result = await Mediator.Send(new SaveDispute.Command(command, UserId)).ConfigureAwait(false);
        return await CreatedAtActionResult(result, nameof(Get));
    }

    /// <summary>
    /// Deletes a dispute from the system
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await Mediator.Send(new DeleteDispute.Command(id)).ConfigureAwait(false);
        return await NoContentResult(result);
    }
}