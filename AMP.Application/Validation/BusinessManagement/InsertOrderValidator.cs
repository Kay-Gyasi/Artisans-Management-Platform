using AMP.Application.Features.Queries.BusinessManagement;
using FluentValidation;

namespace AMP.Application.Validation.BusinessManagement;

public class InsertOrderValidator : AbstractValidator<InsertOrder.Command>
{
    public InsertOrderValidator()
    {
        RuleFor(x => x.Payload.UserId)
            .NotNull()
            .NotEmpty()
            .NotEqual("string");
        RuleFor(x => x.Payload.Description)
            .NotNull()
            .NotEmpty()
            .NotEqual("string");
        RuleFor(x => x.Payload.ServiceId)
            .NotNull()
            .NotEmpty()
            .NotEqual("string");
    }
}