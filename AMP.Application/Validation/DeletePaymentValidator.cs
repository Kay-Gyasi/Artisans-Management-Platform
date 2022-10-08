﻿using AMP.Application.Features.Commands;
using FluentValidation;

namespace AMP.Application.Validation;

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