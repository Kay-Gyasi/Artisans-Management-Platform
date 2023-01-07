using AMP.Application.Features.Commands.Messaging;
using FluentValidation;

namespace AMP.Application.Validation.Messaging;

public class DeleteChatMessageValidator : AbstractValidator<DeleteChatMessage.Command>
{
    public DeleteChatMessageValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .NotEqual("string");
    }
}