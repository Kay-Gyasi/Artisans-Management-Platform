﻿using System;
using AMP.Application.Features.Commands;
using AMP.Domain.Enums;
using FluentValidation;

namespace AMP.Application.Validation
{
    public class PostUserValidator : AbstractValidator<PostUser.Command>
    {
        public PostUserValidator()
        {
            RuleFor(x => x.UserCommand.FirstName)
                .NotEmpty()
                .NotNull()
                .Must(x => x != "string");
            RuleFor(x => x.UserCommand.FamilyName)
                .NotEmpty()
                .NotNull()
                .Must(x => x != "string");
            RuleFor(x => x.UserCommand.Contact.PrimaryContact)
                .NotEmpty()
                .NotNull()
                .Must(x => x != "string");
            RuleFor(x => x.UserCommand.Password)
                .NotEmpty()
                .NotNull()
                .Must(x => x != "string");
            RuleFor(x => x.UserCommand.Type)
                .Must(x => x is UserType.Artisan or UserType.Customer);
        }
    }
}