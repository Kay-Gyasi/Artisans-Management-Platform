using AMP.Application.Features.Commands.BusinessManagement;
using FluentValidation;

namespace AMP.Application.Validation.BusinessManagement;

public class OrderCostValidator : AbstractValidator<OrderCost.Command>
{
    public OrderCostValidator()
    {
        RuleFor(x => x.CostCommand.OrderId)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
        RuleFor(x => x.CostCommand.Cost)
            .Must(x => x >= 0);
    }
}