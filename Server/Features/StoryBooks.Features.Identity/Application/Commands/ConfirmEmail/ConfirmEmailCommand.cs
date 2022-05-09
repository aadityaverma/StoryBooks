namespace StoryBooks.Features.Identity.Application.Commands.ConfirmEmail;

using MediatR;

using StoryBooks.Features.Application;

public record ConfirmEmailCommand(
    string UserId, 
    string Token) : IRequest<RedirectResult>;