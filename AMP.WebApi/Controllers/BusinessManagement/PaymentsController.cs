using AMP.Application.Features.Commands.BusinessManagement;
using AMP.Application.Features.Queries.BusinessManagement;
using AMP.Processors.Commands.BusinessManagement;
using AMP.Processors.Dtos.BusinessManagement;
using AMP.Processors.PageDtos.BusinessManagement;

namespace AMP.WebApi.Controllers.BusinessManagement;

[Authorize]
public class PaymentsController : BaseControllerv1
{

    /// <summary>
    /// Returns a page of payments made or received by requesting user
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPage(PaginatedCommand command)
    {
        var result = await Mediator
            .Send(new GetPaymentPage.Query(command, UserId, Role));
        return Role == "Artisan" ? Ok(result.AsT1) : Ok(result.AsT0);
    }

    /// <summary>
    /// Gets details about a payment
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PaymentDto> Get(string id)
        => await Mediator.Send(new GetPayment.Query(id));

    /// <summary>
    /// Adds a new payment on an order
    /// </summary>
    [Authorize("CustomerOnlyResource")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Save(PaymentCommand command)
    {
        var id = await Mediator.Send(new SavePayment.Command(command));
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    /// <summary>
    /// Sets transaction reference of a payment made on an order
    /// </summary>
    [Authorize("CustomerOnlyResource")]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Verify(VerifyPaymentCommand command)
    {
        await Mediator.Send(new VerifyPayment.Command(command));
        return NoContent();
    }

    /// <summary>
    /// Deletes a payment made on an order
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Delete(string id)
        => await Mediator.Send(new DeletePayment.Command(id));
}