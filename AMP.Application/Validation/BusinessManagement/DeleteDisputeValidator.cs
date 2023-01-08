using AMP.Application.Features.Commands.BusinessManagement;
using FluentValidation;

namespace AMP.Application.Validation.BusinessManagement;

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