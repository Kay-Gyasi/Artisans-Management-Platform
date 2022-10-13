using System.Security.Claims;
using AMP.Application.Features.Commands;
using AMP.Application.Features.Queries;
using AMP.Processors.Commands;
using AMP.Processors.Dtos;
using AMP.Processors.PageDtos;
using AMP.Shared.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace AMP.WebApi.Controllers.v1;

[Authorize]
public class UserController : BaseControllerv1
{
    private string UserId => HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PaginatedList<UserPageDto>> GetPage(PaginatedCommand command)
        => await Mediator.Send(new GetUserPage.Query(command));

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<UserDto> Get(string id)
        => await Mediator.Send(new GetUser.Query(id));

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(UserCommand command)
    {
        var id = await Mediator.Send(new UpdateUser.Command(command));
        return CreatedAtAction(nameof(Get), new {id}, id);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Delete(string id)
        => await Mediator.Send(new DeleteUser.Command(id));

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<SigninResponse> Login(SigninCommand command)
        => await Mediator.Send(new AuthenticateUser.Command(command));
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<SigninResponse> GetRefreshToken()
        => await Mediator.Send(new RefreshToken.Command(UserId));
    
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