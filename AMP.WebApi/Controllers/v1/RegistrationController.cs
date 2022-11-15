namespace AMP.WebApi.Controllers.v1;

public class RegistrationController : BaseControllerv1
{
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(UserCommand command) 
    {
        var response = await Mediator.Send(new PostUser.Command(command));
        if (response == default) return Conflict();
        return Ok(response);
    }
    
    [HttpGet("{phone}/{code}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Verify(string phone, string code) 
    {
        var response = await Mediator.Send(new VerifyUser.Query(phone, code));
        if (!response) return NotFound();
        return Ok();
    }
    
    [HttpGet("{phone}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task SendCode(string phone) 
    {
        await Mediator.Send(new SendVerificationCode.Command(phone));
    }
    
    
}