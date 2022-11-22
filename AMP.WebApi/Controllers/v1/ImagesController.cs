namespace AMP.WebApi.Controllers.v1;

[Authorize]
public class ImagesController : BaseControllerv1
{

    /// <summary>
    /// Updates user's profile image info
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Upload([FromForm(Name = "uploadedFile")] IFormFile file) //naming
    {
        
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var result = await Mediator.Send(new UploadImage.Command(file, userId));
        return result ? NoContent() : BadRequest();
    }
}