using AMP.Application.Features.Commands;
using AMP.Application.Features.Commands.UserManagement;
using FluentValidation;

namespace AMP.Application.Validation;

public class DeleteUserValidator : AbstractValidator<DeleteUser.Command>
{
    public DeleteUserValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
    }
}