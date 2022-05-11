namespace StoryBooks.Features.Application.Messages;

public record BecomeAuthorMessage(
    string UserId,
    string Email,
    string Name,
    string Alias
);
