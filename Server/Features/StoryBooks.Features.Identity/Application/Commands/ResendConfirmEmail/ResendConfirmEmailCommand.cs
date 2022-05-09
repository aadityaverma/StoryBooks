namespace StoryBooks.Features.Identity.Application.Commands.ResendConfirmEmail;

using MediatR;

using StoryBooks.Features.Application;

public record ResendConfirmEmailCommand : IRequest<Result>;