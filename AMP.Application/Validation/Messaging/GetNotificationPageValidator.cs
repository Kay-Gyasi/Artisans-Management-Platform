using AMP.Application.Features.Queries.Messaging;
using FluentValidation;

namespace AMP.Application.Validation.Messaging;

public class GetNotificationPageValidator : AbstractValidator<GetNotificationPage.Query>
{
    public GetNotificationPageValidator()
    {
        RuleFor(x => x.Paginated)
            .NotNull();
    }
}