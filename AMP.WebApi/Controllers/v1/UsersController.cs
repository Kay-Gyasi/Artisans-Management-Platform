namespace AMP.WebApi.Controllers.v1;

[Authorize]
public class UsersController : BaseControllerv1
{
    private string UserId => HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    
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
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(UserCommand command)
    {
        var id = await Mediator.Send(new UpdateUser.Command(command));
        return CreatedAtAction(nameof(Get), new {id}, id);
    }

    /// <summary>
    /// Removes a user from the system
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Delete(string id)
        => await Mediator.Send(new DeleteUser.Command(id));

    /// <summary>
    /// Returns an authorization token which a user can use to access resources on the system 
    /// </summary>
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<SigninResponse> Login(SigninCommand command)
        => await Mediator.Send(new AuthenticateUser.Command(command));
    
    /// <summary>
    /// Returns a refresh token for user
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<SigninResponse> GetRefreshToken()
        => await Mediator.Send(new RefreshToken.Command(UserId));
    
    /// <summary>
    /// Sends a link by which user can reset their password
    /// </summary>
    [AllowAnonymous]
    [HttpGet("{phone}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SendPasswordResetLink(string phone) 
    {
        var response = await Mediator.Send(new SendPassResetLink.Command(phone));
        if (!response) return NotFound();
        return Ok();
    }

    /// <summary>
    /// Resets requesting user's password
    /// </summary>
    [AllowAnonymous]
    [HttpGet("{phone}/{confirmCode}/{newPassword}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ResetPassword(string phone, string confirmCode, string newPassword) 
    {
        var response = await Mediator.Send(new ResetPassword.Command(new ResetPasswordCommand
        {
            Phone = phone,
            ConfirmCode = confirmCode,
            NewPassword = newPassword
        }));
        if (!response) return NotFound();
        return Ok();
    }
}