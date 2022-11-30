using AMP.WebApi.Controllers.v1.Base;

namespace AMP.WebApi.Controllers.v1
{
    public class AdministrationsControllerv1 : BaseControllerv1
    {
        /// <summary>
        /// Sets up all required resources the API needs to run (Including the database)
        /// </summary>
        [Authorize(Roles = "Developer")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task Initialize()
            => await Mediator.Send(new Initialize.Command());
    }
}
