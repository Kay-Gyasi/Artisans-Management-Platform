using AMP.Application.Features.Commands.BusinessManagement;
using FluentValidation;

namespace AMP.Application.Validation.BusinessManagement;

public class DeleteOrderValidator : AbstractValidator<DeleteOrder.Command>
{
    public DeleteOrderValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
    }
}