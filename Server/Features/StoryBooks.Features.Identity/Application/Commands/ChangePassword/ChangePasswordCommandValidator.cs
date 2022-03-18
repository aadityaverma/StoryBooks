namespace StoryBooks.Features.Identity.Application.Commands.ChangePassword;

using FluentValidation;

using Microsoft.Extensions.Options;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator(IOptions<IdentitySettings> options)
    {
        var settings = options.Value;

        RuleFor(m => m.Id)
            .NotEmpty();

        RuleFor(m => m.Password)
            .MinimumLength(settings.MinPasswordLength)
            .NotEmpty();

        RuleFor(m => m.NewPassword)
            .MinimumLength(settings.MinPasswordLength)
            .NotEmpty();
    }
}
