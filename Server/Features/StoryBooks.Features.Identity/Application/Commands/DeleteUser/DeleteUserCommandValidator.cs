namespace StoryBooks.Features.Identity.Application.Commands.DeleteUser;

using FluentValidation;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        this.RuleFor(m => m.Password)
            .NotEmpty();
    }
}
