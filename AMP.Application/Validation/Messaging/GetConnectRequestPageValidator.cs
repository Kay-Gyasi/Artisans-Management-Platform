using AMP.Application.Features.Queries.Messaging;
using FluentValidation;

namespace AMP.Application.Validation.Messaging;

public class GetConnectRequestPageValidator : AbstractValidator<GetConnectRequestPage.Query>
{
    public GetConnectRequestPageValidator()
    {
        RuleFor(x => x.Paginated)
            .NotNull();
    }
}