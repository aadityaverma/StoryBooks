namespace StoryBooks.Features.Authors.Domain.Exceptions;

using StoryBooks.Features.Domain.Exceptions;

internal class TagNotFoundException : EntityNotFoundException
{
    public TagNotFoundException() { }

    public TagNotFoundException(string error) => this.Error = error;
}