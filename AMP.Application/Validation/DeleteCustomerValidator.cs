using AMP.Application.Features.Commands;
using FluentValidation;

namespace AMP.Application.Validation;

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