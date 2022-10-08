using AMP.Application.Features.Commands;
using FluentValidation;

namespace AMP.Application.Validation;

public class DeleteDisputeValidator : AbstractValidator<DeleteDispute.Command>
{
    public DeleteDisputeValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
    }
}