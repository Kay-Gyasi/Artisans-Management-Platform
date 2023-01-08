using AMP.Application.Features.Commands.BusinessManagement;
using FluentValidation;

namespace AMP.Application.Validation.BusinessManagement;

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