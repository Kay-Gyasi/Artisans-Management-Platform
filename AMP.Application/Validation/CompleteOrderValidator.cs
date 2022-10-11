﻿using AMP.Application.Features.Commands;
using FluentValidation;

namespace AMP.Application.Validation;

public class CompleteOrderValidator : AbstractValidator<CompleteOrder.Command>
{
    public CompleteOrderValidator()
    {
        RuleFor(x => x.OrderId)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
    }
}