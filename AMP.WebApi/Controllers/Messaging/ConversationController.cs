using AMP.Application.Features.Commands.Messaging;
using AMP.Application.Features.Queries.Messaging;
using AMP.Processors.Commands.Messaging;

namespace AMP.WebApi.Controllers.Messaging;

public class ConversationController : BaseControllerv1
{
    /// <summary>
    /// Saves a new conversation
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
    public async Task<IActionResult> Save([FromBody] ConversationCommand command)
    {
        var result = await Mediator.Send(new SaveConversation.Command(command)).ConfigureAwait(false);
        return await OkResult(result);
    }

    /// <summary>
    /// Returns details about a conversation
    /// </summary>
    /// <response code="200">Request execution was successful and appropriate response has been returned</response>
    /// <response code="404">Request execution was successful but no data was found</response>
    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string id)
    {
        var result = await Mediator.Send(new GetConversation.Query(id)).ConfigureAwait(false);
        return await OkResult(result);
    }

    /// <summary>
    /// Returns a page of all conversations made by user
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
        var result = await Mediator.Send(new GetConversationPage.Query(command));
        return await OkResult(result);
    }
    
    /// <summary>
    /// Deletes a conversation
    /// </summary>
    /// <response code="204">Conversation has been deleted successfully</response>
    /// <response code="404">Conversation with id provided does not exist</response>
    /// <response code="403">You do not have permission to delete the conversation</response>
    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await Mediator.Send(new DeleteConversation.Command(id)).ConfigureAwait(false);
        return await NoContentResult(result);
    }
    
    /// <summary>
    /// Marks conversation messages as read by receiver
    /// </summary>
    /// <response code="204">Conversation has been updated successfully</response>
    /// <response code="404">Conversation with id provided does not exist</response>
    /// <response code="403">You do not have permission to read the conversation</response>
    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> MarkAsRead(string id)
    {
        var result = await Mediator.Send(new MarkConvoAsReadConversation.Command(id)).ConfigureAwait(false);
        return await NoContentResult(result);
    }
}