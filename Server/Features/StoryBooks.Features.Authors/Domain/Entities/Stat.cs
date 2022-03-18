namespace StoryBooks.Features.Authors.Domain.Entities;

using StoryBooks.Features.Authors.Domain.Exceptions;
using StoryBooks.Features.Domain.Entities;
using StoryBooks.Libraries.Validation;

using System.Diagnostics.CodeAnalysis;

using static StoryBooks.Features.Authors.Domain.AuthorDomainConstants;

public class Stat : Entity<int>
{
    private readonly HashSet<Battle> battles;

    internal Stat(
        string name,
        decimal value,
        bool isCritical,
        decimal minimumValue,
        string groupName,
        Book book)
    {
        Validate(name, groupName);

        this.Name = name;
        this.Value = value;
        this.IsCritical = isCritical;
        this.MinimumValue = minimumValue;
        this.GroupName = groupName;
        this.Book = book;

        battles = new HashSet<Battle>();
    }

    [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "For EF")]
    private Stat(
        string name,
        decimal value,
        bool isCritical,
        decimal minimumValue,
        string groupName)
    {
        this.Name = name;
        this.Value = value;
        this.IsCritical = isCritical;
        this.MinimumValue = minimumValue;
        this.GroupName = groupName;

        this.Book = default!;

        battles = new HashSet<Battle>();
    }

    public string Name { get; private set; }

    public decimal Value { get; private set; }

    public bool IsCritical { get; private set; }

    public decimal MinimumValue { get; private set; }

    public string GroupName { get; private set; }

    public Book Book { get; private set; }

    public IReadOnlyCollection<Battle> Battles => this.battles.ToList().AsReadOnly();

    internal Stat UpdateName(string name)
    {
        ValidateName(name, nameof(name));

        this.Name = name;
        return this;
    }

    internal Stat UpdateValue(decimal value, decimal minValue)
    {
        this.Value = value;
        this.MinimumValue = minValue;
        return this;
    }

    internal Stat UpdateGroupName(string groupName)
    {
        ValidateName(groupName, nameof(groupName));

        this.GroupName = groupName;
        return this;
    }

    private static void Validate(string name, string groupName)
    {
        Validate(name, nameof(name));
        Validate(groupName, nameof(groupName));
    }

    private static void ValidateName(string name, string propName)
    {
        Guard.ForStringLength<InvalidStatException>(
            value: name,
            minLength: MinStatNameLength,
            maxLength: MaxStatNameLength,
            name: propName);
    }
}