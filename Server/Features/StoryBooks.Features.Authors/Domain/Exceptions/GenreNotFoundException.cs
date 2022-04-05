namespace StoryBooks.Features.Authors.Domain.Exceptions;

using StoryBooks.Features.Domain.Exceptions;

internal class GenreNotFoundException : EntityNotFoundException
{
    public GenreNotFoundException() { }

    public GenreNotFoundException(string error) => this.Error = error;
}