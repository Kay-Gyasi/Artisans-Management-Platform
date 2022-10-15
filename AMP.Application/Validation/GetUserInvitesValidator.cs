using AMP.Application.Features.Queries;
using FluentValidation;

namespace AMP.Application.Validation;

public class GetUserInvitesValidator : AbstractValidator<GetUserInvites.Query>
{
    public GetUserInvitesValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .NotNull()
            .Must(a => a.Length <= 36);
    }
}