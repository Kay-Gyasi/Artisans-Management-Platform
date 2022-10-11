using AMP.Application.Features.Commands;
using FluentValidation;

namespace AMP.Application.Validation;

public class SaveDisputeValidator : AbstractValidator<SaveDispute.Command>
{
    public SaveDisputeValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
        RuleFor(x => x.DisputeCommand.OrderId)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
        RuleFor(x => x.DisputeCommand.Details)
            .NotNull()
            .NotEmpty();
    }
}