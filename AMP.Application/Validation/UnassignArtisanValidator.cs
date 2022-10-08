using AMP.Application.Features.Commands;
using FluentValidation;

namespace AMP.Application.Validation;

public class UnassignArtisanValidator : AbstractValidator<UnassignArtisan.Command>
{
    public UnassignArtisanValidator()
    {
        RuleFor(x => x.OrderId)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
    }
}