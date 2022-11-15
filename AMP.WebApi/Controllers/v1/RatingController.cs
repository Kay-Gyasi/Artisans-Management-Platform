﻿namespace AMP.WebApi.Controllers.v1;

[Authorize]
public class RatingController : BaseControllerv1
{
    private string UserId => HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PaginatedList<RatingPageDto>> GetCustomerPage(PaginatedCommand command)
        => await Mediator.Send(new GetRatingPage.Query(command, UserId));
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PaginatedList<RatingPageDto>> GetArtisanRatingPage(PaginatedCommand command)
        => await Mediator.Send(new GetArtisanRatingPage.Query(command, UserId));

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<RatingDto> Get(string id)
        => await Mediator.Send(new GetRating.Query(id));

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Save(RatingCommand command)
    {
        command.UserId = UserId;
        var id = await Mediator.Send(new SaveRating.Command(command));
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Delete(string id)
        => await Mediator.Send(new DeleteRating.Command(id));
}