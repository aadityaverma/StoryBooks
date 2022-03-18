namespace StoryBooks.Features.Common.Application.Interfaces;

public interface ICurrentUser
{
    string UserId { get; }

    string Email { get; }

    string FirstName { get; }

    string LastName { get; }

    string FullName { get; }

    IList<string> Roles { get; }
}