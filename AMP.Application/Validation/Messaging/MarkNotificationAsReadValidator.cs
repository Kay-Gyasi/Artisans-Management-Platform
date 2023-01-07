using AMP.Application.Features.Commands.Messaging;
using FluentValidation;

namespace AMP.Application.Validation.Messaging;

public class MarkNotificationAsReadValidator : AbstractValidator<MarkNotificationAsRead.Command>
{
    public MarkNotificationAsReadValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEqual("string")
            .NotEmpty();
    }
}