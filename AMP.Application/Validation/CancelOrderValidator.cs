using AMP.Application.Features.Commands;
using FluentValidation;

namespace AMP.Application.Validation;

public class CancelOrderValidator : AbstractValidator<CancelOrder.Command>
{
    public CancelOrderValidator()
    {
        RuleFor(x => x.OrderId)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
    }
}