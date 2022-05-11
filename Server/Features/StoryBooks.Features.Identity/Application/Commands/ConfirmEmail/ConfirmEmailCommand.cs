namespace StoryBooks.Features.Identity.Application.Commands.ConfirmEmail;

public record ConfirmEmailCommand(
    string UserId, 
    string Token) : IRequest<RedirectResult>;