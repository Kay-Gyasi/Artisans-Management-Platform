using AMP.Application.Features.Queries;
using AMP.Domain.ViewModels;
using AMP.Processors.Processors;
using Microsoft.AspNetCore.Authorization;

namespace AMP.WebApi.Controllers.v1;

[Authorize]
public class LookupController : BaseControllerv1
{

    [HttpGet("{type}")]
    [ProducesResponseType((StatusCodes.Status200OK))]
    [ProducesResponseType((StatusCodes.Status404NotFound))]
    public async Task<List<Lookup>> Get(LookupType type)
        => await Mediator.Send(new GetLookup.Query(type));
}