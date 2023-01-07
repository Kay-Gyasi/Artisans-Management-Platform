using AMP.Application.Features.Queries.Messaging;
using FluentValidation;

namespace AMP.Application.Validation.Messaging;

public class GetConversationValidator : AbstractValidator<GetConversation.Query>
{
    public GetConversationValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotEqual("string")
            .NotNull();
    }
}