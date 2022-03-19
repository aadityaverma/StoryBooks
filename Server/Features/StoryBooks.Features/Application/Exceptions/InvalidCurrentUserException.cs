namespace StoryBooks.Features.Application.Exceptions;

using StoryBooks.Features.Domain.Exceptions;

internal class InvalidCurrentUserException : BaseDomainException
{
    public InvalidCurrentUserException()
    { }

    public InvalidCurrentUserException(string error) => this.Error = error;
}
