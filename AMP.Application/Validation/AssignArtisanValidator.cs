using AMP.Application.Features.Commands;
using FluentValidation;

namespace AMP.Application.Validation;

public class AssignArtisanValidator : AbstractValidator<AssignArtisan.Command>
{
    public AssignArtisanValidator()
    {
        RuleFor(x => x.ArtisanId)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
        RuleFor(x => x.OrderId)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
    }
}