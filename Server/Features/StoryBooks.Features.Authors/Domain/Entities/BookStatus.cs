namespace StoryBooks.Features.Authors.Domain.Entities;

using StoryBooks.Features.Domain.Entities;

using System.Diagnostics.CodeAnalysis;

public class BookStatus : Enumeration
{
    [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "For EF")]
    private BookStatus(int value) : base(value, FromValue<BookStatus>(value).Name)
    {
    }

    private BookStatus(int value, string name) : base(value, name)
    {
    }

    public static readonly BookStatus Draft = new(1, nameof(Draft));
    public static readonly BookStatus Published = new(2, nameof(Published));
    public static readonly BookStatus Archived = new(3, nameof(Archived));
}