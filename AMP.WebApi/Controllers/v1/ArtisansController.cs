using AMP.WebApi.Controllers.v1.Base;

namespace AMP.WebApi.Controllers.v1;

[Authorize]
public class ArtisansController : BaseControllerv1
{

    /// <summary>
    /// Returns a page of all active artisans (For use by administrators only)
    /// </summary>
    [Authorize(Roles = "Administrator")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PaginatedList<ArtisanPageDto>> GetPage([FromBody] PaginatedCommand command)
        => await Mediator.Send(new GetArtisanPage.Query(command));

    /// <summary>
    /// Returns info about an artisan
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ArtisanDto> Get(string id)
        => await Mediator.Send(new GetArtisan.Query(id));

    /// <summary>
    /// Returns info about an artisan
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ArtisanDto> GetByUser()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return await Mediator.Send(new GetArtisanByUser.Query(userId));
    }
    
    /// <summary>
    /// Returns all artisans who have worked for a requesting customer
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<List<Lookup>> GetArtisansWorkedForCustomer()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return await Mediator.Send(new GetArtisansWhoHaveWorkedForCustomer.Query(userId));
    }

    /// <summary>
    /// Adds or updates info about an artisan
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Save(ArtisanCommand command)
    {
        var id = await Mediator.Send(new SaveArtisan.Command(command));
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    /// <summary>
    /// Removes an artisan from the system
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Delete(string id)
        => await Mediator.Send(new DeleteArtisan.Command(id));
}