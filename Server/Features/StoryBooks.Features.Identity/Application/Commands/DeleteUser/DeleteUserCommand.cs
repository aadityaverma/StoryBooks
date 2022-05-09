namespace StoryBooks.Features.Identity.Application.Commands.DeleteUser;

using MediatR;

using StoryBooks.Features.Application;

public record DeleteUserCommand(
    string Password) : IRequest<Result>;