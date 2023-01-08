using AMP.Application.Features.Commands.UserManagement;
using AMP.Domain.Enums;
using FluentValidation;

namespace AMP.Application.Validation.UserManagement
{
    public class PostUserValidator : AbstractValidator<PostUser.Command>
    {
        public PostUserValidator()
        {
            RuleFor(x => x.UserCommand.FirstName)
                .NotEmpty()
                .NotNull()
                .NotEqual("string");
            RuleFor(x => x.UserCommand.FamilyName)
                .NotEmpty()
                .NotNull()
                .NotEqual("string");
            RuleFor(x => x.UserCommand.Contact.PrimaryContact)
                .NotEmpty()
                .NotNull()
                .NotEqual("string");
            RuleFor(x => x.UserCommand.Contact)
                .NotNull();
            RuleFor(x => x.UserCommand.Password)
                .NotEmpty()
                .NotNull()
                .NotEqual("string");
            RuleFor(x => x.UserCommand.Type)
                .Must(x => x is UserType.Artisan or UserType.Customer);
        }
    }
}