using AMP.Application.Features.Commands.UserManagement;
using FluentValidation;

namespace AMP.Application.Validation.UserManagement;

public class AuthenticateUserValidator : AbstractValidator<AuthenticateUser.Command>
{
    public AuthenticateUserValidator()
    {
        RuleFor(x => x.Signin.Password)
            .NotNull()
            .NotEmpty();
        RuleFor(x => x.Signin.Phone)
            .NotNull()
            .NotEmpty();
    }
}