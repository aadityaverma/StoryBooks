namespace StoryBooks.Features.Identity.Application.Commands.ChangePassword;

public record ChangePasswordCommand(
    string Id, 
    string Password, 
    string NewPassword) : IRequest<Result>;