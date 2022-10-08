using AMP.Application.Features.Commands;
using FluentValidation;

namespace AMP.Application.Validation;

public class DeleteServiceValidator : AbstractValidator<DeleteService.Command>
{
    public DeleteServiceValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
    }
}