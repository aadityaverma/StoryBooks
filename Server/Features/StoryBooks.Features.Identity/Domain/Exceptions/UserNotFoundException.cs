namespace StoryBooks.Features.Identity.Domain.Exceptions;

using StoryBooks.Features.Domain.Exceptions;

public class UserNotFoundException : BaseDomainException
{
    public UserNotFoundException()
    { }

    public UserNotFoundException(string error) => this.Error = error;
}