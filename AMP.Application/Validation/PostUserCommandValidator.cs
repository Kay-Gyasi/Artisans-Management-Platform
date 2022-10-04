using AMP.Application.Features.Commands;
using AMP.Domain.Enums;
using FluentValidation;

namespace AMP.Application.Validation
{
    public class PostUserCommandValidator : AbstractValidator<PostUser.Command>
    {
        public PostUserCommandValidator()
        {
            RuleFor(x => x.UserCommand.FirstName)
                .NotEmpty()
                .NotNull()
                .Must(x => x != "string");
            RuleFor(x => x.UserCommand.FamilyName)
                .NotEmpty()
                .NotNull()
                .Must(x => x != "string");
            RuleFor(x => x.UserCommand.Contact.PrimaryContact)
                .NotEmpty()
                .NotNull()
                .Must(x => x != "string");
            RuleFor(x => x.UserCommand.Password)
                .NotEmpty()
                .NotNull()
                .Must(x => x != "string");
            RuleFor(x => x.UserCommand.Type)
                .Must(x => x == UserType.Artisan || x == UserType.Customer);
        }
    }
}