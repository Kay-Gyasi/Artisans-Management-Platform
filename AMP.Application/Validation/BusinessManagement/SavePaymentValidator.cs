using AMP.Application.Features.Commands.BusinessManagement;
using FluentValidation;

namespace AMP.Application.Validation.BusinessManagement;

public class SavePaymentValidator : AbstractValidator<SavePayment.Command>
{
    public SavePaymentValidator()
    {
        RuleFor(x => x.PaymentCommand.OrderId)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
        RuleFor(x => x.PaymentCommand.Reference)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
        RuleFor(x => x.PaymentCommand.AmountPaid)
            .NotNull()
            .NotEmpty()
            .Must(x => x > 0);
    }
}