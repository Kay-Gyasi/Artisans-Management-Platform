using AMP.Application.Features.Commands.Messaging;
using FluentValidation;

namespace AMP.Application.Validation.Messaging;

public class AcceptConnectRequestValidator : AbstractValidator<AcceptConnectRequest.Command>
{
    public AcceptConnectRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotEqual("string")
            .NotNull();
    }
}