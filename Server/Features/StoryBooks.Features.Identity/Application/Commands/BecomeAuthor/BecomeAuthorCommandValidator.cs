namespace StoryBooks.Features.Identity.Application.Commands.BecomeAuthor;

using FluentValidation;

using static StoryBooks.Features.Identity.Domain.IdentityDomainConstants;

public class BecomeAuthorCommandValidator : AbstractValidator<BecomeAuthorCommand>
{
    public BecomeAuthorCommandValidator()
    {
        this.RuleFor(m => m.Alias)
            .NotEmpty()
            .MinimumLength(Validation.MinNameLength)
            .MaximumLength(Validation.MaxNameLength);
    }
}
