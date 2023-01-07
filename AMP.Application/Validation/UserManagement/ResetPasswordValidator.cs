using AMP.Application.Features.Commands;
using AMP.Application.Features.Commands.UserManagement;
using FluentValidation;

namespace AMP.Application.Validation;

public class ResetPasswordValidator : AbstractValidator<ResetPassword.Command>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x.ResetCommand.Phone)
            .NotEmpty()
            .NotNull()
            .Must(x => x.Length <= 15);
        RuleFor(x => x.ResetCommand.ConfirmCode)
            .NotEmpty()
            .NotNull();
        RuleFor(x => x.ResetCommand.NewPassword)
            .NotEmpty()
            .NotNull();
    }
}