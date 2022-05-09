namespace StoryBooks.Features.Identity.Application.Commands.ChangePassword;

using MediatR;

using StoryBooks.Features.Application;

public record ChangePasswordCommand(
    string Id, 
    string Password, 
    string NewPassword) : IRequest<Result>;