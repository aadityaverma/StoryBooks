namespace StoryBooks.Features.Identity.Application.Commands.ChangePassword;

using FluentValidation;

using Microsoft.Extensions.Options;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator(IOptions<IdentitySettings> options)
    {
        var settings = options.Value;

        this.RuleFor(m => m.Id)
            .NotEmpty();

        this.RuleFor(m => m.Password)
            .MinimumLength(settings.MinPasswordLength)
            .NotEmpty();

        this.RuleFor(m => m.NewPassword)
            .MinimumLength(settings.MinPasswordLength)
            .NotEmpty();
    }
}
