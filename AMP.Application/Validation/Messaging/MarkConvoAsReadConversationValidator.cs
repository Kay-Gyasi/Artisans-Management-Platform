using AMP.Application.Features.Commands.Messaging;
using FluentValidation;

namespace AMP.Application.Validation.Messaging;

public class MarkConvoAsReadConversationValidator : AbstractValidator<MarkConvoAsReadConversation.Command>
{
    public MarkConvoAsReadConversationValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual("string")
            .NotEmpty()
            .NotNull();
    }
}