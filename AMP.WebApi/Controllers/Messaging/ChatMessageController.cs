using AMP.Application.Features.Commands.Messaging;
using AMP.Processors.Commands.Messaging;

namespace AMP.WebApi.Controllers.Messaging;

public class ChatMessageController : BaseControllerv1
{
    /// <summary>
    /// Sends a new chat
    /// </summary>
    /// <response code="200">Message has been sent successfully</response>
    /// <response code="404">Chat send failed</response>
    /// <response code="403">You do not have permission to access this resource</response>
    /// <response code="412">Input is missing some required fields</response>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
    public async Task<IActionResult> Save([FromBody] ChatMessageCommand command)
    {
        var result = await Mediator.Send(new SaveChatMessage.Command(command)).ConfigureAwait(false);
        return await OkResult(result);
    }

    /// <summary>
    /// Deletes a chat
    /// </summary>
    /// <response code="204">Chat has been deleted successfully</response>
    /// <response code="404">Chat with id provided does not exist</response>
    /// <response code="403">You do not have permission to delete the chat</response>
    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await Mediator.Send(new DeleteChatMessage.Command(id)).ConfigureAwait(false);
        return await NoContentResult(result);
    }

}