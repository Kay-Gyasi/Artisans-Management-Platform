using AMP.Application.Features.Commands.BusinessManagement;
using FluentValidation;

namespace AMP.Application.Validation.BusinessManagement;

public class DeleteRatingValidator : AbstractValidator<DeleteRating.Command>
{
    public DeleteRatingValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
    }
}