using AMP.Application.Features.Commands.UserManagement;
using AMP.Processors.Commands.UserManagement;

namespace AMP.WebApi.Controllers.v1;

[Authorize]
public class ImagesController : BaseControllerv1
{

    /// <summary>
    /// Updates user's profile image info
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(SigninResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Upload([FromForm(Name = "file")] IFormFile file) //naming
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var response = await Mediator.Send(new UploadImage.Command(file, userId));
        return response is not null ? Ok(response) : BadRequest();
    }
}