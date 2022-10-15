﻿using AMP.Application.Features.Commands;
using AMP.Domain.Enums;
using FluentValidation;

namespace AMP.Application.Validation;

public class AddInviteValidator : AbstractValidator<AddInvite.Command>
{
    public AddInviteValidator()
    {
        RuleFor(x => x.Invite.Type)
            .Must(a => a is UserType.Artisan or UserType.Customer)
            .NotNull();
        RuleFor(x => x.Invite.InvitedPhone)
            .Must(a => a.Length <= 15)
            .NotNull()
            .NotEmpty();
        RuleFor(x => x.UserId)
            .Must(a => a.Length <= 36)
            .NotNull()
            .NotEmpty();
    }
}