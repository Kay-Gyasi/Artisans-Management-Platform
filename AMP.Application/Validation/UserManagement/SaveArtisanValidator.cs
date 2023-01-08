using AMP.Application.Features.Commands.UserManagement;
using FluentValidation;

namespace AMP.Application.Validation.UserManagement;

public class SaveArtisanValidator : AbstractValidator<SaveArtisan.Command>
{
    public SaveArtisanValidator()
    {
        RuleFor(x => x.ArtisanCommand.BusinessName)
            .NotNull()
            .NotEmpty()
            .NotEqual("string");
    }
}