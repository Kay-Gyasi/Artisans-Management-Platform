using AMP.Application.Features.Queries.Messaging;
using FluentValidation;

namespace AMP.Application.Validation.Messaging;

public class GetConversationPageValidator : AbstractValidator<GetConversationPage.Query>
{
    public GetConversationPageValidator()
    {
        RuleFor(x => x.Paginated)
            .NotNull();
    }
}