using AMP.Application.Features.Commands.Messaging;
using FluentValidation;

namespace AMP.Application.Validation.Messaging;

public class DeleteConnectRequestValidator : AbstractValidator<DeleteConnectRequest.Command>
{
    public DeleteConnectRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotEqual("string")
            .NotNull();
    }
}