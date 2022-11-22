namespace AMP.WebApi.Controllers.v1;

[Authorize]
public class RatingsController : BaseControllerv1
{
    private string UserId => HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    /// <summary>
    /// Returns a page of reviews made by requesting customer
    /// </summary>
    [Authorize(Roles = "Customer")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PaginatedList<RatingPageDto>> GetCustomerPage(PaginatedCommand command)
        => await Mediator.Send(new GetRatingPage.Query(command, UserId));
    
    /// <summary>
    /// Returns a page of reviews made on an artisan
    /// </summary>
    [Authorize(Roles = "Artisan")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PaginatedList<RatingPageDto>> GetArtisanRatingPage(PaginatedCommand command)
        => await Mediator.Send(new GetArtisanRatingPage.Query(command, UserId));

    /// <summary>
    /// Returns details about a review
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<RatingDto> Get(string id)
        => await Mediator.Send(new GetRating.Query(id));

    /// <summary>
    /// Adds or updates info on a review made by customer
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Save(RatingCommand command)
    {
        command.UserId = UserId;
        var id = await Mediator.Send(new SaveRating.Command(command));
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    /// <summary>
    /// Deletes a review
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Delete(string id)
        => await Mediator.Send(new DeleteRating.Command(id));
}