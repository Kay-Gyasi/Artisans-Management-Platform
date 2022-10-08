using AMP.Application.Features.Commands;
using AMP.Domain.Enums;
using FluentValidation;

namespace AMP.Application.Validation;

public class ArtisanCompleteOrderValidator : AbstractValidator<ArtisanCompleteOrder.Command>
{
    public ArtisanCompleteOrderValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .NotNull()
            .Must(x => x != "string");
    }   
}