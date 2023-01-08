using AMP.Application.Features.Queries.UserManagement;
using FluentValidation;

namespace AMP.Application.Validation.UserManagement;

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