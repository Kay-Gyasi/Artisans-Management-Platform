using AMP.Application.Features.Commands.UserManagement;
using FluentValidation;

namespace AMP.Application.Validation.UserManagement;

public class SoftDeleteUserValidator : AbstractValidator<SoftDeleteUser.Command>
{
    public SoftDeleteUserValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
    }
}

public class DeleteUserValidator : AbstractValidator<DeleteUser.Command>
{
    public DeleteUserValidator()
    {
        RuleFor(x => x.Phone)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
    }
}