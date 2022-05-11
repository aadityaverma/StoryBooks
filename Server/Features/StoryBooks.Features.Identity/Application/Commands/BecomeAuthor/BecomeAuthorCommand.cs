namespace StoryBooks.Features.Identity.Application.Commands.BecomeAuthor;

public record BecomeAuthorCommand(
    string Alias) : IRequest<Result>;