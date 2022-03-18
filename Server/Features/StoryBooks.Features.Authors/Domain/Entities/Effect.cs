namespace StoryBooks.Features.Authors.Domain.Entities;

using StoryBooks.Features.Authors.Domain.Exceptions;
using StoryBooks.Features.Common.Domain.Entities;
using StoryBooks.Libraries.Validation;

using System.Diagnostics.CodeAnalysis;

using static StoryBooks.Features.Authors.Domain.AuthorDomainConstants;

public class Effect : Entity<int>
{
    private readonly HashSet<StatModifier> modifiers;

    internal Effect(string text, Chapter chapter)
    {
        ValidateText(text);

        this.Text = text;
        this.Chapter = chapter;

        this.modifiers = new HashSet<StatModifier>();
    }

    [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "For EF")]
    private Effect(string text)
    {
        this.Text = text;
        this.Chapter = default!;
        this.modifiers = new HashSet<StatModifier>();
    }

    public string Text { get; private set; }

    public Chapter Chapter { get; private set; }

    public IReadOnlyCollection<StatModifier> StatModifiers => this.modifiers.ToList().AsReadOnly();

    internal Effect UpdateText(string text)
    {
        ValidateText(text);

        this.Text = text;
        return this;
    }

    internal Effect AddStatModifier(decimal change, Stat stat)
    {
        this.modifiers.Add(new StatModifier(change, stat));
        return this;
    }

    internal Effect RemoveStatModifier(StatModifier modifier)
    {
        this.modifiers.Remove(modifier);
        return this;
    }

    private static void ValidateText(string text)
    {
        Guard.ForStringLength<InvalidEffectException>(
            value: text,
            minLength: MinEffectTextLength,
            maxLength: MaxEffectTextLength,
            name: nameof(text));
    }
}
