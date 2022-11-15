﻿namespace AMP.WebApi.Controllers.v1;

[Authorize]
public class ImageController : BaseControllerv1
{

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