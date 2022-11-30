using AMP.WebApi.Controllers.v1.Base;

namespace AMP.WebApi.Controllers.v1;

public class RegistrationsController : BaseControllerv1
{
    /// <summary>
    /// Adds a new unverified account to the system
    /// </summary>
    /// <response code="201">User has been created and added to system successfully</response>
    /// <response code="409">A user with the contact provided already exists in the system</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Post(UserCommand command)
    {
        var id = await Mediator.Send(new PostUser.Command(command));
        return Created($"{ApiUrl}/registrations/Get/{0}", id);
    }
    
    /// <summary>
    /// Verifies an account
    /// </summary>
    /// <response code="200">User has been verified successfully</response>
    /// <response code="404">The phone/verification code provided was invalid</response>
    [HttpGet("{phone}/{code}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Verify(string phone, string code) 
    {
        await Mediator.Send(new VerifyUser.Query(phone, code));
        return Ok();
    }
    
    /// <summary>
    /// Sends a verification link by sms to requesting user
    /// </summary>
    /// <response code="200">Verification link has been sent successfully</response>
    /// <response code="404">The phone provided was invalid</response>
    [HttpGet("{phone}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SendCode(string phone) 
    {
        await Mediator.Send(new SendVerificationCode.Command(phone));
        return Ok();
    }
}