using AMP.Processors.Processors;

namespace AMP.WebApi.Controllers.v1;

[Authorize]
public class LookupController : BaseControllerv1
{
    private string UserId => HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


    [HttpGet("{type}")]
    [ProducesResponseType((StatusCodes.Status200OK))]
    [ProducesResponseType((StatusCodes.Status404NotFound))]
    public async Task<List<Lookup>> Get(LookupType type)
        => await Mediator.Send(new GetLookup.Query(type));
    
    [HttpGet]
    [ProducesResponseType((StatusCodes.Status200OK))]
    [ProducesResponseType((StatusCodes.Status404NotFound))]
    public async Task<List<Lookup>> GetOpenOrdersLookup()
        => await Mediator.Send(new GetOpenOrdersLookup.Query(UserId));
}