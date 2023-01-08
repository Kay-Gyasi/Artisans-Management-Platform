using AMP.Application.Features.Commands.UserManagement;
using FluentValidation;

namespace AMP.Application.Validation.UserManagement;

public class UploadImageValidator : AbstractValidator<UploadImage.Command>
{
    public UploadImageValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .NotEmpty()
            .Must(x => x != "string");
        RuleFor(x => x.File)
            .NotNull();
    }
}