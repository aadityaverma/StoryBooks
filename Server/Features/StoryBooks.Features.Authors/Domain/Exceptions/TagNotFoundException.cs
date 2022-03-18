namespace StoryBooks.Features.Authors.Domain.Exceptions;

using StoryBooks.Features.Common.Domain.Exceptions;

internal class TagNotFoundException : BaseDomainException
{
    public TagNotFoundException() { }

    public TagNotFoundException(string error) => this.Error = error;
}