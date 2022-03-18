namespace StoryBooks.Features.Authors.Domain.Exceptions;

using StoryBooks.Features.Common.Domain.Exceptions;

internal class InvalidBookException : BaseDomainException
{
    public InvalidBookException() { }

    public InvalidBookException(string error) => this.Error = error;
}