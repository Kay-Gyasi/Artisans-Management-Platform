using AMP.Application.Features.Commands.Messaging;
using FluentValidation;

namespace AMP.Application.Validation.Messaging;

public class DeleteConversationValidator : AbstractValidator<DeleteConversation.Command>
{
    public DeleteConversationValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual("string")
            .NotEmpty()
            .NotNull();
    }
}