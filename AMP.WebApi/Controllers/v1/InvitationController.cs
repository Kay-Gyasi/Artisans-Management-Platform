namespace AMP.WebApi.Controllers.v1;

[Authorize]
public class InvitationController : BaseControllerv1
{
    private string UserId => HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddInvite(InvitationCommand command)
    {
        var response = await Mediator.Send(new AddInvite.Command(command, UserId));
        if (!response) return BadRequest();
        return NoContent();
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<List<InvitationDto>> GetUserInvites()
        => await Mediator.Send(new GetUserInvites.Query(UserId));
}