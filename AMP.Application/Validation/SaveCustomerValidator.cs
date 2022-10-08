using AMP.Application.Features.Commands;
using FluentValidation;

namespace AMP.Application.Validation;

public class SaveCustomerValidator : AbstractValidator<SaveCustomer.Command>
{
    public SaveCustomerValidator()
    {
        RuleFor(x => x.CustomerCommand.UserId)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
    }
}