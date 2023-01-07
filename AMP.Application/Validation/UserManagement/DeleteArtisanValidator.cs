using AMP.Application.Features.Commands;
using AMP.Application.Features.Commands.UserManagement;
using FluentValidation;

namespace AMP.Application.Validation;

public class DeleteArtisanValidator : AbstractValidator<DeleteArtisan.Command>
{
    public DeleteArtisanValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
    }
}