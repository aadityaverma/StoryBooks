namespace StoryBooks.Features.Identity.Application.Commands.DeleteUser;

public record DeleteUserCommand(
    string Password) : IRequest<Result>;