using AMP.Application.Features.Commands.BusinessManagement;
using FluentValidation;

namespace AMP.Application.Validation.BusinessManagement;

public class SaveOrderValidator : AbstractValidator<SaveOrder.Command>
{
    public SaveOrderValidator()
    {
        RuleFor(x => x.OrderCommand.UserId)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
        RuleFor(x => x.OrderCommand.Id)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
        RuleFor(x => x.OrderCommand.Description)
            .NotNull()
            .NotEmpty();
        RuleFor(x => x.OrderCommand.ServiceId)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
    }
}