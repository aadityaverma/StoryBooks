namespace StoryBooks.Features.Identity.Application.Commands.RegisterUser;

using FluentValidation;

using Microsoft.Extensions.Options;

using static StoryBooks.Features.Identity.Domain.IdentityDomainConstants;
using static StoryBooks.Libraries.Validation.CommonValidationConstants;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator(IOptions<IdentitySettings> options)
    {
        var settings = options.Value;

        this.RuleFor(u => u.Email)
            .Matches(Email.RegularExpression)
            .MinimumLength(Email.MinLength)
            .MaximumLength(Email.MaxLength)
            .EmailAddress()
            .NotEmpty();

        this.RuleFor(u => u.Password)
            .MinimumLength(settings.MinPasswordLength)
            .NotEmpty();

        this.RuleFor(u => u.ConfirmPassword)
            .Equal(u => u.Password)
            .WithMessage(ErrorMessages.ConfirmPasswordNotMatching);

        this.RuleFor(u => u.FirstName)
            .MinimumLength(Validation.MinNameLength)
            .MaximumLength(Validation.MaxNameLength)
            .NotEmpty();

        this.RuleFor(u => u.LastName)
            .MinimumLength(Validation.MinNameLength)
            .MaximumLength(Validation.MaxNameLength)
            .NotEmpty();
    }
}