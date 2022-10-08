using AMP.Application.Features.Commands;
using FluentValidation;

namespace AMP.Application.Validation;

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