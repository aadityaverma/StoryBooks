namespace StoryBooks.Features.Identity.Application.Commands.ConfirmEmail;

using FluentValidation;

public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailCommandValidator()
    {
        this.RuleFor(m => m.UserId)
            .NotEmpty();

        this.RuleFor(m => m.Token)
            .NotEmpty();
    }
}