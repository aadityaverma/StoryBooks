namespace StoryBooks.Features.Identity.Domain.Exceptions;

using StoryBooks.Features.Domain.Exceptions;

internal class InvalidUserNameException : BaseDomainException
{
    public InvalidUserNameException()
    { }

    public InvalidUserNameException(string error) => this.Error = error;
}