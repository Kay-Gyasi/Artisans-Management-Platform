using AMP.Application.Features.Commands.UserManagement;
using FluentValidation;

namespace AMP.Application.Validation.UserManagement;

public class UpdateUserValidator : AbstractValidator<UpdateUser.Command>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.UserCommand.FirstName)
            .NotEmpty()
            .NotNull()
            .Must(x => x != "string");
        RuleFor(x => x.UserCommand.FamilyName)
            .NotEmpty()
            .NotNull()
            .Must(x => x != "string");
        RuleFor(x => x.UserCommand.Contact.PrimaryContact)
            .NotEmpty()
            .NotNull()
            .Must(x => x != "string");
    }
}