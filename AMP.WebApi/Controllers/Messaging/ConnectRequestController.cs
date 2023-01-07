using AMP.Application.Features.Commands.Messaging;
using AMP.Application.Features.Queries.Messaging;
using AMP.Processors.Commands.Messaging;

namespace AMP.WebApi.Controllers.Messaging;

public class ConnectRequestController : BaseControllerv1
{
    /// <summary>
    /// Makes a request to connect for chatting
    /// </summary>
    /// <response code="200">Request has been made successfully</response>
    /// <response code="404">Request failed</response>
    /// <response code="403">You do not have permission to access this resource</response>
    /// <response code="412">Input is missing some required fields</response>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
    public async Task<IActionResult> Save([FromBody] ConnectRequestCommand command)
    {
        var result = await Mediator.Send(new SaveConnectRequest.Command(command)).ConfigureAwait(false);
        return await OkResult(result);
    }

    /// <summary>
    /// Returns details about a request to connect for chatting
    /// </summary>
    /// <response code="200">Request execution was successful and appropriate response has been returned</response>
    /// <response code="404">Request execution was successful but no data was found</response>
    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string id)
    {
        var result = await Mediator.Send(new GetConnectRequest.Query(id)).ConfigureAwait(false);
        return await OkResult(result);
    }

    /// <summary>
    /// Returns a page of all requests made by user
    /// </summary>
    /// <response code="200">Operation completed successfully</response>
    /// <response code="404">Operation failed</response>
    /// <response code="403">You do not have permission to access this resource</response>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetPage(PaginatedCommand command)
    {
        var result = await Mediator.Send(new GetConnectRequestPage.Query(command));
        return await OkResult(result);
    }

    /// <summary>
    /// Accepts a request
    /// </summary>
    /// <response code="204">Request has been accepted successfully</response>
    /// <response code="404">Request with id provided does not exist</response>
    /// <response code="403">You do not have permission to accept the request</response>
    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Accept(string id)
    {
        var result = await Mediator.Send(new AcceptConnectRequest.Command(id)).ConfigureAwait(false);
        return await NoContentResult(result);
    }

    /// <summary>
    /// Deletes a request
    /// </summary>
    /// <response code="204">Request has been deleted successfully</response>
    /// <response code="404">Request with id provided does not exist</response>
    /// <response code="403">You do not have permission to delete the request</response>
    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await Mediator.Send(new DeleteConnectRequest.Command(id)).ConfigureAwait(false);
        return await NoContentResult(result);
    }
}