namespace StoryBooks.Features.Authors.Domain.Exceptions;

using StoryBooks.Features.Domain.Exceptions;

internal class InvalidAuthorException : BaseDomainException
{
    public InvalidAuthorException() { }

    public InvalidAuthorException(string error) => this.Error = error;
}