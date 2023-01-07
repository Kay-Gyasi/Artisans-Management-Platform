using AMP.Processors.Hubs;
using Microsoft.AspNet.SignalR;

namespace AMP.WebApi.Controllers.v1.Base
{
    
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [Produces("application/json", "application/problem+json")]
    public abstract class BaseControllerv1 : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected static string ApiUrl => "https://artisan-management-platform";
        protected string UserId => HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        protected string Role => HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

        protected Task<IActionResult> OkResult<T>(Result<T> result)
        {
            return Task.Run(() => result.Match(b => Ok(b),
                BuildProblemDetails));
        }
        
        protected Task<IActionResult> NoContentResult<T>(Result<T> result)
        {
            return Task.Run(() => result.Match(b => NoContent(),
                BuildProblemDetails));
        }
        
        protected Task<IActionResult> CreatedResult(Result<string> result, string route)
        {
            return Task.Run(() => result.Match(b => Created(route, b),
                BuildProblemDetails));
        }
        
        protected Task<IActionResult> CreatedAtActionResult(Result<string> result, string actionName)
        {
            return Task.Run(() => result.Match(b => 
                    CreatedAtAction(actionName, new {id = b}, b),
                BuildProblemDetails));
        }

        private IActionResult BuildProblemDetails(Exception ex)
            => Problem(title: ex.Message, statusCode: ex.GetStatusCode());
    }
}