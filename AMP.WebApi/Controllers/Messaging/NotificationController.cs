using AMP.Application.Features.Commands.Messaging;
using AMP.Application.Features.Queries.Messaging;

namespace AMP.WebApi.Controllers.Messaging;

public class NotificationController : BaseControllerv1
{
    /// <summary>
    /// Returns details about a notification
    /// </summary>
    /// <response code="200">Request execution was successful and appropriate response has been returned</response>
    /// <response code="404">Request execution was successful but no data was found</response>
    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string id)
    {
        var result = await Mediator.Send(new GetNotification.Query(id)).ConfigureAwait(false);
        return await OkResult(result);
    }

    /// <summary>
    /// Returns a page of all notifications for user
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
        var result = await Mediator.Send(new GetNotificationPage.Query(command));
        return await OkResult(result);
    }
    
    /// <summary>
    /// Marks a notification as read
    /// </summary>
    /// <response code="204">Notification has been marked successfully</response>
    /// <response code="404">Notificaton with id provided does not exist</response>
    /// <response code="403">You do not have permission to mark this notification</response>
    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> MarkAsRead(string id)
    {
        var result = await Mediator.Send(new MarkNotificationAsRead.Command(id)).ConfigureAwait(false);
        return await NoContentResult(result);
    }

    /// <summary>
    /// Deletes a notification
    /// </summary>
    /// <response code="204">Notification has been deleted successfully</response>
    /// <response code="404">Notification with id provided does not exist</response>
    /// <response code="403">You do not have permission to delete the notification</response>
    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await Mediator.Send(new DeleteNotification.Command(id)).ConfigureAwait(false);
        return await NoContentResult(result);
    }
}