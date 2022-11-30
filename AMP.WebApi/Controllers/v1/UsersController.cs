using AMP.WebApi.Controllers.v1.Base;

namespace AMP.WebApi.Controllers.v1;

[Authorize]
public class UsersController : BaseControllerv1
{
    
    /// <summary>
    /// Returns a page of users on the system
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PaginatedList<UserPageDto>> GetPage(PaginatedCommand command)
        => await Mediator.Send(new GetUserPage.Query(command));

    /// <summary>
    /// Returns details of a user
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<UserDto> Get(string id)
        => await Mediator.Send(new GetUser.Query(id));

    /// <summary>
    /// Updates a user's info
    /// </summary>
    /// <response code="200">User has been updated successfully</response>
    /// <response code="404">User being considered does not exist</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(UserCommand command)
    {
        var id = await Mediator.Send(new UpdateUser.Command(command));
        return CreatedAtAction(nameof(Get), new {id}, id);
    }

    /// <summary>
    /// Removes a user from the system
    /// </summary>
    /// <response code="204">User has been deleted successfully</response>
    /// <response code="404">User with id provided does not exist</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        await Mediator.Send(new DeleteUser.Command(id));
        return NoContent();
    }

    /// <summary>
    /// Returns an authorization token which a user can use to access resources on the system 
    /// </summary>
    /// <response code="200">Log in was successful</response>
    /// <response code="204">Log in was not successful because of invalid credentials</response>
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<SigninResponse> Login(SigninCommand command)
        => await Mediator.Send(new AuthenticateUser.Command(command));

    /// <summary>
    /// Sends a link by which user can reset their password
    /// </summary>
    /// <response code="200">Password reset link has been sent successfully</response>
    /// <response code="404">User with phone provided does not exist</response>
    [AllowAnonymous]
    [HttpGet("{phone}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task SendPasswordResetLink(string phone) 
        => await Mediator.Send(new SendPassResetLink.Command(phone));

    /// <summary>
    /// Resets requesting user's password
    /// </summary>
    /// <response code="200">Password has been reset successfully</response>
    /// <response code="404">Invalid phone/code</response>
    [AllowAnonymous]
    [HttpGet("{phone}/{confirmCode}/{newPassword}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task ResetPassword(string phone, string confirmCode, string newPassword) 
    {
        await Mediator.Send(new ResetPassword.Command(new ResetPasswordCommand
        {
            Phone = phone,
            ConfirmCode = confirmCode,
            NewPassword = newPassword
        }));
    }
}