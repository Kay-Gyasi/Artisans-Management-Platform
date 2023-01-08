using AMP.Application.Features.Queries.BusinessManagement;
using AMP.Processors.Processors;

namespace AMP.WebApi.Controllers.v1;

[Authorize]
public class LookupsController : BaseControllerv1
{

    /// <summary>
    /// Returns a lookup of requested type
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    [HttpGet("{type}")]
    [ProducesResponseType((StatusCodes.Status200OK))]
    [ProducesResponseType((StatusCodes.Status404NotFound))]
    public async Task<List<Lookup>> Get(LookupType type)
        => await Mediator.Send(new GetLookup.Query(type));
    
    /// <summary>
    /// Returns a lookup of all orders placed by requesting user
    /// </summary>
    [HttpGet]
    [ProducesResponseType((StatusCodes.Status200OK))]
    [ProducesResponseType((StatusCodes.Status404NotFound))]
    public async Task<List<Lookup>> GetOpenOrdersLookup()
        => await Mediator.Send(new GetOpenOrdersLookup.Query(UserId));
}