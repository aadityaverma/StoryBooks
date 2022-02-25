namespace StoryBooks.Features.Identity.Application.Commands.LoginUser
{
    using FluentValidation;

    using Microsoft.Extensions.Options;

    using StoryBooks.Libraries.Validation;

    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator(IOptions<IdentitySettings> options)
        {
            var settings = options.Value;

            this.RuleFor(u => u.Email)
                .MinimumLength(CommonValidationConstants.Email.MinLength)
                .MaximumLength(CommonValidationConstants.Email.MaxLength)
                .EmailAddress()
                .NotEmpty();

            this.RuleFor(u => u.Password)
                .MinimumLength(settings.MinPasswordLength)
                .NotEmpty();
        }
    }
}
