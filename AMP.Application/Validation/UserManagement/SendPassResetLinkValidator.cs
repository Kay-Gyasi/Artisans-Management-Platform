using AMP.Application.Features.Commands;
using AMP.Application.Features.Commands.UserManagement;
using FluentValidation;

namespace AMP.Application.Validation;

public class SendPassResetLinkValidator : AbstractValidator<SendPassResetLink.Command>
{
    public SendPassResetLinkValidator()
    {
        RuleFor(x => x.Phone)
            .NotNull()
            .NotEmpty()
            .Must(x => x.Length <= 15);
    }
}