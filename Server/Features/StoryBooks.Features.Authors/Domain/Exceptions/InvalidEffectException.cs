namespace StoryBooks.Features.Authors.Domain.Exceptions;

using StoryBooks.Features.Common.Domain.Exceptions;

internal class InvalidEffectException : BaseDomainException
{
    public InvalidEffectException() { }

    public InvalidEffectException(string error) => this.Error = error;
}