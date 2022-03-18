namespace StoryBooks.Features.Authors.Domain.Entities;

using StoryBooks.Features.Common.Domain.Entities;

public class BookCover : Image
{
    internal BookCover(string name, byte[] content)
        : base(name, content)
    {
    }
}