namespace AMP.WebApi.Controllers.Base
{
    
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [Produces("application/json", "application/problem+json")]
    public abstract class BaseControllerv1 : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}