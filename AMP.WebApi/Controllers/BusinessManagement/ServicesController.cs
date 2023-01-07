using AMP.Application.Features.Commands.BusinessManagement;
using AMP.Processors.Commands.BusinessManagement;
using AMP.Processors.Dtos.BusinessManagement;
using AMP.Processors.PageDtos.BusinessManagement;

namespace AMP.WebApi.Controllers.v1;

[Authorize]
public class ServicesController : BaseControllerv1
{

    /// <summary>
    /// Returns a page of all services being offered by artisans on the system 
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PaginatedList<ServicePageDto>> GetPage(PaginatedCommand command)
        => await Mediator.Send(new GetServicePage.Query(command));

    /// <summary>
    /// Returns details about a service being offered on the system
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ServiceDto> Get(string id)
        => await Mediator.Send(new GetService.Query(id));

    /// <summary>
    /// Adds a new service to the system
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Save(ServiceCommand command)
    {
        var id = await Mediator.Send(new SaveService.Command(command));
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    /// <summary>
    /// Deletes a service being offered on the system
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Delete(string id)
        => await Mediator.Send(new DeleteService.Command(id));
}