using AMP.Application.Features.Commands.BusinessManagement;
using FluentValidation;

namespace AMP.Application.Validation.BusinessManagement;

public class DeletePaymentValidator : AbstractValidator<DeletePayment.Command>
{
    public DeletePaymentValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
    }
}