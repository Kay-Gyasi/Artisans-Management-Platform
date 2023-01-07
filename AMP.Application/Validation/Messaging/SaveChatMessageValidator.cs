using AMP.Application.Features.Commands.Messaging;
using FluentValidation;

namespace AMP.Application.Validation.Messaging;

public class SaveChatMessageValidator : AbstractValidator<SaveChatMessage.Command>
{
    public SaveChatMessageValidator()
    {
        RuleFor(x => x.Payload.ConversationId)
            .NotNull()
            .NotEmpty()
            .NotEqual("string");
        RuleFor(x => x.Payload.ReceiverId)
            .NotNull()
            .NotEmpty()
            .NotEqual("string");
    }
}