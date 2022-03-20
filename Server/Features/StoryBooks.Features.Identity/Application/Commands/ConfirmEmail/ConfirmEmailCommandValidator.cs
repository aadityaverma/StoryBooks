namespace StoryBooks.Features.Identity.Application.Commands.ConfirmEmail;

using FluentValidation;

public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailCommandValidator()
    {
        RuleFor(m => m.UserId)
            .NotEmpty();

        RuleFor(m => m.Token)
            .NotEmpty();
    }
}