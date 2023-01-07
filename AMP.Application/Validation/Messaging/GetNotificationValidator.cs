using AMP.Application.Features.Queries.Messaging;
using FluentValidation;

namespace AMP.Application.Validation.Messaging;

public class GetNotificationValidator : AbstractValidator<GetNotification.Query>
{
    public GetNotificationValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual("string")
            .NotNull()
            .NotEmpty();
    }
}