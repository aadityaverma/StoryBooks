namespace StoryBooks.Features.Authors.Domain.Exceptions;

using StoryBooks.Features.Domain.Exceptions;

internal class InvalidStatException : BaseDomainException
{
    public InvalidStatException() { }

    public InvalidStatException(string error) => this.Error = error;
}