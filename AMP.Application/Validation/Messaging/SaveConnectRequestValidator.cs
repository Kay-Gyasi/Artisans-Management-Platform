using AMP.Application.Features.Commands.Messaging;
using FluentValidation;

namespace AMP.Application.Validation.Messaging;

public class SaveConnectRequestValidator : AbstractValidator<SaveConnectRequest.Command>
{
    public SaveConnectRequestValidator()
    {
        RuleFor(x => x.Payload.InviteeId)
            .NotEqual("string")
            .NotEmpty()
            .NotNull();
    }
}