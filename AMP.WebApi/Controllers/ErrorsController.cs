using Microsoft.AspNetCore.Diagnostics;

namespace AMP.WebApi.Controllers;

[ApiController]
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        return Problem(title: exception?.Message, statusCode: (int)HttpStatusCode.PreconditionFailed);
    }
}