using AMP.Application.Features.Commands.Messaging;
using FluentValidation;

namespace AMP.Application.Validation.Messaging;

public class SaveConversationValidator : AbstractValidator<SaveConversation.Command>
{
    public SaveConversationValidator()
    {
        RuleFor(x => x.Payload.SecondParticipantId)
            .NotEmpty()
            .NotNull()
            .NotEqual("string");
    }
}