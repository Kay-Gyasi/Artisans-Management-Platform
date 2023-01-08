using AMP.Application.Features.Commands.UserManagement;
using AMP.Application.Features.Queries.UserManagement;
using AMP.Processors.Commands.UserManagement;
using AMP.Processors.PageDtos.UserManagement;

namespace AMP.WebApi.Controllers.v1;

[Authorize]
public class ArtisansController : BaseControllerv1
{

    /// <summary>
    /// Returns a page of all available artisans for a specific service
    /// </summary>
    /// <response code="200">Operation completed successfully</response>
    /// <response code="403">You do not have permission to access this resource</response>
    [Authorize("CustomerAdminDevResource")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<PaginatedList<ArtisanPageDto>> GetPage([FromBody] PaginatedCommand command)
        => await Mediator.Send(new GetArtisanPage.Query(command));

    /// <summary>
    /// Returns info about an artisan
    /// </summary>
    /// <response code="200">Request execution was successful and appropriate response has been returned</response>
    /// <response code="404">Request execution was successful but no data was found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string id)
    {
        var result = await Mediator.Send(new GetArtisan.Query(id)).ConfigureAwait(false);
        return await OkResult(result);
    }

    /// <summary>
    /// Returns info about an artisan
    /// </summary>
    /// <response code="200">Request execution was successful and appropriate response has been returned</response>
    /// <response code="404">Request execution was successful but no data was found</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByUser()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var result = await Mediator.Send(new GetArtisanByUser.Query(userId)).ConfigureAwait(false);
        return await OkResult(result);
    }
    
    /// <summary>
    /// Returns all artisans who have worked for a requesting customer
    /// </summary>
    /// <response code="200">Request execution was successful and appropriate response has been returned</response>
    /// <response code="403">You do not have permission to access this resource</response>
    [Authorize("CustomerAdminDevResource")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetArtisansWorkedForCustomer()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var result = await Mediator.Send(new GetArtisansWhoHaveWorkedForCustomer.Query(userId)).ConfigureAwait(false);
        return await OkResult(result);
    }

    /// <summary>
    /// Adds or updates info about an artisan
    /// </summary>
    /// <response code="201">Artisan has been created successfully</response>
    /// <response code="403">You do not have permission to access this resource</response>
    [Authorize("ArtisanAdminDevResource")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Save(ArtisanCommand command)
    {
        var result = await Mediator.Send(new SaveArtisan.Command(command)).ConfigureAwait(false);
        return await CreatedAtActionResult(result, nameof(Get));
    }

    /// <summary>
    /// Removes an artisan from the system
    /// </summary>
    /// <response code="204">Artisan has been deleted successfully</response>
    /// <response code="404">Artisan with id provided does not exist</response>
    /// <response code="403">You do not have permission to access this resource</response>
    [Authorize("ArtisanAdminDevResource")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await Mediator.Send(new DeleteArtisan.Command(id)).ConfigureAwait(false);
        return await NoContentResult(result);
    }
}