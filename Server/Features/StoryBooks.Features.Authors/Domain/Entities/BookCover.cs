namespace StoryBooks.Features.Authors.Domain.Entities;

using StoryBooks.Features.Domain.Entities;

public class BookCover : Image
{
    internal BookCover(string name, byte[] content)
        : base(name, content)
    {
    }
}