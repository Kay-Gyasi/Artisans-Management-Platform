using AMP.Application.Features.Commands.BusinessManagement;
using FluentValidation;

namespace AMP.Application.Validation.BusinessManagement;

public class VerifyPaymentValidator : AbstractValidator<VerifyPayment.Command>
{
    public VerifyPaymentValidator()
    {
        RuleFor(x => x.Payment.Reference)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
        RuleFor(x => x.Payment.TransactionReference)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
    }
}