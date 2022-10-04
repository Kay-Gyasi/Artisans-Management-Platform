using System.Security.Claims;
using AMP.Application.Features.Commands;
using AMP.Application.Features.Queries;
using AMP.Processors.Commands;
using AMP.Processors.Dtos;
using AMP.Processors.PageDtos;
using AMP.Shared.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace AMP.WebApi.Controllers.v1;

[Authorize]
public class DisputeController : BaseControllerv1
{
    private string UserId => HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PaginatedList<DisputePageDto>> GetPage(PaginatedCommand command)
        => await Mediator.Send(new GetDisputePage.Query(command));

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<DisputeDto> Get(string id)
        => await Mediator.Send(new GetDispute.Query(id));
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<DisputeCount> GetOpenDisputeCount()
        => await Mediator.Send(new GetOpenDisputeCount.Query(UserId));

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Save(DisputeCommand command)
    {
        var id = await Mediator.Send(new SaveDispute.Command(command, UserId));
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Delete(string id)
        => await Mediator.Send(new DeleteDispute.Command(id));
}