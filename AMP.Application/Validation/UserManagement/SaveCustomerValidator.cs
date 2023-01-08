using AMP.Application.Features.Commands.UserManagement;
using FluentValidation;

namespace AMP.Application.Validation.UserManagement;

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