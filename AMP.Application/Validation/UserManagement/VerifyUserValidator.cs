using AMP.Application.Features.Queries;
using FluentValidation;

namespace AMP.Application.Validation;

public class VerifyUserValidator : AbstractValidator<VerifyUser.Query>
{
    public VerifyUserValidator()
    {
        RuleFor(x => x.Code)
            .NotNull()
            .NotEmpty()
            .Must(x => x.Length == 20);
    }
}