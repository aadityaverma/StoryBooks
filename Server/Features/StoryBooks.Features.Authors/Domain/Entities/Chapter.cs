namespace StoryBooks.Features.Authors.Domain.Entities;

using StoryBooks.Features.Authors.Domain.Exceptions;
using StoryBooks.Features.Domain.Entities;
using StoryBooks.Libraries.Validation;

using System.Diagnostics.CodeAnalysis;

using static StoryBooks.Features.Authors.Domain.AuthorDomainConstants;

public class Chapter : Entity<int>
{
    private readonly HashSet<Effect> effects;
    private readonly HashSet<Choice> choices;

    internal Chapter(
        string title,
        string content,
        Book book,
        bool isFirstChapter = false,
        bool isCheckpoint = false,
        bool isDiceChoice = false)
    {
        this.Title = title;
        this.Content = content;
        this.IsFirstChapter = isFirstChapter;
        this.IsCheckpoint = isCheckpoint;
        this.IsDiceChoice = isDiceChoice;

        this.Book = book;

        this.Choice = default!;
        this.Battle = default!;

        this.effects = new HashSet<Effect>();
        this.choices = new HashSet<Choice>();
    }

    [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "For EF")]
    private Chapter(
        string title,
        string content,
        bool isFirstChapter,
        bool isCheckpoint,
        bool isDiceChoice)
    {
        ValidateTitle(title);

        this.Title = title;
        this.Content = content;
        this.IsFirstChapter = !isFirstChapter;
        this.IsCheckpoint = isCheckpoint;
        this.IsDiceChoice = isDiceChoice;

        this.Book = default!;
        this.Choice = default!;
        this.Battle = default!;

        this.effects = new HashSet<Effect>();
        this.choices = new HashSet<Choice>();
    }

    public string Title { get; private set; }

    public string Content { get; private set; }

    public bool IsFirstChapter { get; private set; }

    public bool IsCheckpoint { get; private set; }

    public bool IsDiceChoice { get; private set; }

    public Book Book { get; private set; }

    public Battle? Battle { get; private set; }

    public Choice? Choice { get; private set; }

    public IReadOnlyCollection<Effect> Effects => this.effects.ToList().AsReadOnly();

    public IReadOnlyCollection<Choice> Choices => this.choices.ToList().AsReadOnly();

    internal Chapter UpdateTitle(string title)
    {
        ValidateTitle(title);

        this.Title = title;
        return this;
    }

    internal Chapter UpdateContent(string content)
    {
        this.Content = content;
        return this;
    }

    internal Chapter SetAsFirstChapter()
    {
        this.IsFirstChapter = true;
        return this;
    }

    internal Chapter UnsetAsFirstChapter()
    {
        this.IsCheckpoint = false;
        return this;
    }

    internal Chapter SetAsCheckpoint()
    {
        this.IsCheckpoint = true;
        return this;
    }

    internal Chapter UnsetAsCheckpoint()
    {
        this.IsCheckpoint = false;
        return this;
    }

    internal Chapter SetChoiceWithDice()
    {
        this.IsDiceChoice = true;
        return this;
    }

    internal Chapter UnsetChoiceWithDice()
    {
        this.IsDiceChoice = false;
        return this;
    }

    internal Chapter AddBattleSettings(string enemyName, bool includeDiceInBattle)
    {
        this.Battle = new Battle(enemyName, includeDiceInBattle);
        return this;
    }

    internal Chapter RemoveBattleSettings()
    {
        this.Battle = null;
        return this;
    }

    internal Chapter AddEffect(string effectText)
    {
        this.effects.Add(new Effect(effectText, this));
        return this;
    }

    internal Chapter RemoveEffect(Effect effect)
    {
        this.effects.Remove(effect);
        return this;
    }

    internal Chapter ClearEffects()
    {
        this.effects.Clear();
        return this;
    }

    internal Chapter AddCoice(string text, Chapter nextChapter)
    {
        this.choices.Add(new Choice(text, nextChapter));
        return this;
    }

    internal Chapter RemoveChoice(Choice choice)
    {
        this.choices.Remove(choice);
        return this;
    }

    internal Chapter ClearChoices()
    {
        this.choices.Clear();
        return this;
    }

    private static void ValidateTitle(string title)
    {
        Guard.ForStringLength<InvalidChapterException>(
            value: title,
            minLength: MinChapterTitleLength,
            maxLength: MaxChapterTitleLength,
            name: nameof(title));
    }
}
