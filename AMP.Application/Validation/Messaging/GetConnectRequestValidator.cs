using AMP.Application.Features.Queries.Messaging;
using FluentValidation;

namespace AMP.Application.Validation.Messaging;

public class GetConnectRequestValidator : AbstractValidator<GetConnectRequest.Query>
{
    public GetConnectRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEqual("string")
            .NotEmpty();
    }
}