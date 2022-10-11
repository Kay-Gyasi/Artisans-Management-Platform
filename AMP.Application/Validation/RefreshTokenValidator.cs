﻿using AMP.Application.Features.Commands;
using FluentValidation;

namespace AMP.Application.Validation;

public class RefreshTokenValidator : AbstractValidator<RefreshTokenCommand.Command>
{
    public RefreshTokenValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
    }
}