using AMP.Application.Features.Commands.UserManagement;
using FluentValidation;

namespace AMP.Application.Validation.UserManagement;

public class DeleteRegistrationValidator : AbstractValidator<DeleteRegistration.Command>
{
    public DeleteRegistrationValidator()
    {
        RuleFor(x => x.Phone)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
    }
}