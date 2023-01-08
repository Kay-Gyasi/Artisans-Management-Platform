using AMP.Application.Features.Commands.BusinessManagement;
using FluentValidation;

namespace AMP.Application.Validation.BusinessManagement;

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