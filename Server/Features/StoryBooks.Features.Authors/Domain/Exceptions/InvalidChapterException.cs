namespace StoryBooks.Features.Authors.Domain.Exceptions;

using StoryBooks.Features.Common.Domain.Exceptions;

internal class InvalidChapterException : BaseDomainException
{
    public InvalidChapterException() { }

    public InvalidChapterException(string error) => this.Error = error;
}