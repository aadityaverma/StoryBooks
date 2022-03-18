namespace StoryBooks.Features.Identity.Application.Commands.LoginUser;

using FluentValidation;

using Microsoft.Extensions.Options;

using static StoryBooks.Libraries.Validation.CommonValidationConstants;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator(IOptions<IdentitySettings> options)
    {
        var settings = options.Value;

        this.RuleFor(u => u.Email)
            .NotEmpty()
            .Matches(Email.RegularExpression)
            .MinimumLength(Email.MinLength)
            .MaximumLength(Email.MaxLength)
            .EmailAddress();

        this.RuleFor(u => u.Password)
            .NotEmpty();
    }
}