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
    }
}