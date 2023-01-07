using AMP.Application.Features.Commands.Messaging;
using FluentValidation;

namespace AMP.Application.Validation.Messaging;

public class DeleteNotificationValidator : AbstractValidator<DeleteNotification.Command>
{
    public DeleteNotificationValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEqual("string")
            .NotEmpty();
    }
}