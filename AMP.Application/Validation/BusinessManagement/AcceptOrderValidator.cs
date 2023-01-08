using AMP.Application.Features.Commands.BusinessManagement;
using FluentValidation;

namespace AMP.Application.Validation.BusinessManagement;

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