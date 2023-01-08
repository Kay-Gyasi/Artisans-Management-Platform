using AMP.Application.Features.Commands.BusinessManagement;
using FluentValidation;

namespace AMP.Application.Validation.BusinessManagement;

public class SaveServiceValidator : AbstractValidator<SaveService.Command>
{
    public SaveServiceValidator()
    {
        RuleFor(x => x.ServiceCommand.Name)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
    }
}