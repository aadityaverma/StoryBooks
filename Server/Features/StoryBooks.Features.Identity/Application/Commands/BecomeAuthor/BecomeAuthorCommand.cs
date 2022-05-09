namespace StoryBooks.Features.Identity.Application.Commands.BecomeAuthor;

using MediatR;

using StoryBooks.Features.Application;

public record BecomeAuthorCommand(
    string Alias) : IRequest<Result>;