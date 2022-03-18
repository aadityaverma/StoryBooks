namespace StoryBooks.Features.Authors.Domain.Exceptions;

using StoryBooks.Features.Domain.Exceptions;

internal class InvalidTagException : BaseDomainException
{
    public InvalidTagException() { }

    public InvalidTagException(string error) => this.Error = error;
}