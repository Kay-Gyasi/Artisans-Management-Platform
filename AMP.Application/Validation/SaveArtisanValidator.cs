using AMP.Application.Features.Commands;
using FluentValidation;

namespace AMP.Application.Validation;

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