using AMP.WebApi.Controllers.v1.Base;

namespace AMP.WebApi.Controllers.v1;

[Authorize]
public class InvitationsController : BaseControllerv1
{
    
    /// <summary>
    /// Sends a membership invite to provided contact in name of requesting user
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddInvite(InvitationCommand command)
    {
        var response = await Mediator.Send(new AddInvite.Command(command, UserId));
        if (!response) return BadRequest();
        return NoContent();
    }
    
    /// <summary>
    /// Returns all invites sent out by requesting user
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<List<InvitationDto>> GetUserInvites()
        => await Mediator.Send(new GetUserInvites.Query(UserId));
}