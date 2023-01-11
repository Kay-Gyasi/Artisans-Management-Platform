﻿using AMP.Application.Features.Commands.UserManagement;
using FluentValidation;

namespace AMP.Application.Validation.UserManagement;

public class DeleteCustomerValidator : AbstractValidator<DeleteCustomer.Command>
{
    public DeleteCustomerValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
    }
}