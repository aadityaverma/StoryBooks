namespace StoryBooks.Features.Identity.Domain.Entities;

using StoryBooks.Features.Domain.Entities;

using System.Diagnostics.CodeAnalysis;

public class Gender : Enumeration
{
    [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "For EF")]
    private Gender(int value) : base(value, FromValue<Gender>(value).Name)
    {
    }

    private Gender(int value, string name) : base(value, name)
    {
    }

    public static readonly Gender Male = new(1, nameof(Male));
    public static readonly Gender Female = new(2, nameof(Female));
    public static readonly Gender Other = new(3, nameof(Other));
}
