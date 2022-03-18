namespace StoryBooks.Features.Authors.Domain.Exceptions;

using StoryBooks.Features.Domain.Exceptions;

internal class InvalidChoiceException : BaseDomainException
{
    public InvalidChoiceException() { }

    public InvalidChoiceException(string error) => this.Error = error;
}