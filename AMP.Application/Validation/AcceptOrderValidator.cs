using AMP.Application.Features.Commands;
using FluentValidation;

namespace AMP.Application.Validation;

public class AcceptOrderValidator : AbstractValidator<AcceptOrder.Command>
{
    public AcceptOrderValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .NotNull()
            .Must(x => x != "string");
    }   
}