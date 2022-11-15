namespace AMP.WebApi.Controllers.v1;

[Authorize]
public class PaymentController : BaseControllerv1
{
    private string UserId => HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    private string Role => HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PaginatedList<PaymentPageDto>> GetPage(PaginatedCommand command)
    {
        return await Mediator.Send(new GetPaymentPage.Query(command, UserId, Role));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<PaymentDto> Get(string id)
        => await Mediator.Send(new GetPayment.Query(id));

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Save(PaymentCommand command)
    {
        var id = await Mediator.Send(new SavePayment.Command(command));
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Verify(VerifyPaymentCommand command)
    {
        await Mediator.Send(new VerifyPayment.Command(command));
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Delete(string id)
        => await Mediator.Send(new DeletePayment.Command(id));
}