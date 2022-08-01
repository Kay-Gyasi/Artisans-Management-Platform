using AMP.Application.Features.Commands;

namespace AMP.WebApi.Controllers.v1
{
    public class AdministrationControllerv1 : BaseControllerv1
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task InitializeDb()
            => await Mediator.Send(new Initialize.Command());
    }
}
