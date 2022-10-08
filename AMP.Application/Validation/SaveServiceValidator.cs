using AMP.Application.Features.Commands;
using FluentValidation;

namespace AMP.Application.Validation;

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