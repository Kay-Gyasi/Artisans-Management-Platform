using AMP.Application.Features.Commands.UserManagement;
using AMP.Application.Features.Queries.UserManagement;
using AMP.Processors.Commands.UserManagement;
using AMP.Processors.PageDtos.UserManagement;

namespace AMP.WebApi.Controllers.v1;

[Authorize]
public class UsersController : BaseControllerv1
{
    
    /// <summary>
    /// Returns a page of users on the system
    /// </summary>
    /// <response code="200">Operation completed successfully</response>
    /// <response code="404">Operation failed</response>
    /// <response code="403">You do not have permission to access this resource</response>
    [Authorize("AdminDevResource")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<PaginatedList<UserPageDto>> GetPage(PaginatedCommand command)
        => await Mediator.Send(new GetUserPage.Query(command));

    /// <summary>
    /// Returns details of a user
    /// </summary>
    /// <response code="200">User found and returned successfully</response>
    /// <response code="404">User with provided id was not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string id)
    {
        var result = await Mediator.Send(new GetUser.Query(id)).ConfigureAwait(false);
        return await OkResult(result);
    }

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
        var result = await Mediator.Send(new UpdateUser.Command(command)).ConfigureAwait(false);
        return await CreatedAtActionResult(result, nameof(Get));
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
        var result = await Mediator.Send(new SoftDeleteUser.Command(id)).ConfigureAwait(false);
        return await NoContentResult(result);
    }

    /// <summary>
    /// Removes a user from the system
    /// </summary>
    /// <response code="204">User has been deleted successfully</response>
    /// <response code="404">User with id provided does not exist</response>
    [HttpDelete("{phone}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> HardDelete(string phone)
    {
        var result = await Mediator.Send(new DeleteUser.Command(phone)).ConfigureAwait(false);
        return await NoContentResult(result);
    }

    /// <summary>
    /// Returns an authorization token which a user can use to access resources on the system 
    /// </summary>
    /// <response code="200">Log in was successful</response>
    /// <response code="204">Log in was not successful because of invalid credentials</response>
    [AllowAnonymous]
    [EnableRateLimiting("Unauthorized")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Login(SigninCommand command)
    {
        var result = await Mediator.Send(new AuthenticateUser.Command(command)).ConfigureAwait(false);
        return result.Match<IActionResult>(Ok, NoContent);
    }

    /// <summary>
    /// Sends a link by which user can reset their password
    /// </summary>
    /// <response code="200">Password reset link has been sent successfully</response>
    /// <response code="404">User with phone provided does not exist</response>
    [AllowAnonymous]
    [HttpGet("{phone}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SendPasswordResetLink(string phone)
    {
        var result = await Mediator.Send(new SendPassResetLink.Command(phone)).ConfigureAwait(false);
        return await OkResult(result);
    }

    /// <summary>
    /// Resets requesting user's password
    /// </summary>
    /// <response code="200">Password has been reset successfully</response>
    /// <response code="404">Invalid phone/code</response>
    [AllowAnonymous]
    [HttpGet("{phone}/{confirmCode}/{newPassword}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ResetPassword(string phone, string confirmCode, string newPassword) 
    {
        var result = await Mediator.Send(new ResetPassword.Command(new ResetPasswordCommand
        {
            Phone = phone,
            ConfirmCode = confirmCode,
            NewPassword = newPassword
        })).ConfigureAwait(false);
        return await OkResult(result);
    }
}