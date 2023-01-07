using AMP.Application.Features.Commands;
using AMP.Application.Features.Commands.BusinessManagement;
using FluentValidation;

namespace AMP.Application.Validation;

public class SaveDisputeValidator : AbstractValidator<SaveDispute.Command>
{
    public SaveDisputeValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .NotEmpty()
            .NotEqual("string");
        RuleFor(x => x.DisputeCommand.OrderId)
            .NotNull()
            .NotEmpty()
            .NotEqual("string");
        RuleFor(x => x.DisputeCommand.Details)
            .NotNull()
            .NotEmpty();
    }
}