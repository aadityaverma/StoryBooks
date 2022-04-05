namespace StoryBooks.Features.Authors.Domain.Exceptions;

using StoryBooks.Features.Domain.Exceptions;

internal class BookNotFoundException : EntityNotFoundException
{
    public BookNotFoundException() { }

    public BookNotFoundException(string error) => this.Error = error;
}