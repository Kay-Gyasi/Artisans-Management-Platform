using AMP.Application.Features.Commands;
using Microsoft.AspNetCore.Authorization;

namespace AMP.WebApi.Controllers.v1
{
    public class AdministrationControllerv1 : BaseControllerv1
    {
        [Authorize(Roles = "Developer")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task InitializeDb()
            => await Mediator.Send(new Initialize.Command());
    }
}
