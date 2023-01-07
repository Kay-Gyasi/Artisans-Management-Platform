using AMP.Application.Features.Commands;
using AMP.Application.Features.Commands.BusinessManagement;
using FluentValidation;

namespace AMP.Application.Validation;

public class SaveRatingValidator : AbstractValidator<SaveRating.Command>
{
    public SaveRatingValidator()
    {
        RuleFor(x => x.RatingCommand.UserId)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
        RuleFor(x => x.RatingCommand.ArtisanId)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
        RuleFor(x => x.RatingCommand.Votes)
            .NotNull()
            .NotEmpty()
            .Must(x => x is >= 0 and <= 5);
    }
}