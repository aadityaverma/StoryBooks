namespace StoryBooks.Features.Authors.Domain.Entities;

using StoryBooks.Features.Authors.Domain.Exceptions;
using StoryBooks.Features.Common.Domain.Entities;
using StoryBooks.Libraries.Validation;

using System.Diagnostics.CodeAnalysis;

using static StoryBooks.Features.Authors.Domain.AuthorDomainConstants;

public class Choice : Entity<int>
{
    internal Choice(string text, Chapter capter)
    {
        ValidateText(text);

        this.Text = text;
        this.NextChapter = capter;
    }

    [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "For EF")]
    private Choice(string text)
    {
        this.Text = text;
        this.NextChapter = default!;
    }

    public string Text { get; private set; }

    public Chapter NextChapter { get; private set; }

    internal Choice UpdateText(string text)
    {
        ValidateText(text);

        this.Text = text;
        return this;
    }

    internal Choice ChangeChapter(Chapter newChapter)
    {
        this.NextChapter = newChapter;
        return this;
    }

    private static void ValidateText(string text)
    {
        Guard.ForStringLength<InvalidChoiceException>(
            value: text,
            minLength: MinChoiceTextLength,
            maxLength: MaxChoiceTextLength,
            name: nameof(text));
    }
}