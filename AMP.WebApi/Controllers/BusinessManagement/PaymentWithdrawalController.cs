using AMP.Application.Features.Commands.BusinessManagement;

namespace AMP.WebApi.Controllers.BusinessManagement;

public class PaymentWithdrawalController : BaseControllerv1
{

    [Authorize("ArtisanOnlyResource")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Save()
    {
        var result = await Mediator.Send(new SaveArtisanWithdrawal.Command());
        return await OkResult(result);
    }
}