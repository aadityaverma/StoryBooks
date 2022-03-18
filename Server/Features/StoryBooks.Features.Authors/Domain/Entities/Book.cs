namespace StoryBooks.Features.Authors.Domain.Entities;

using StoryBooks.Features.Authors.Domain.Exceptions;
using StoryBooks.Features.Common.Domain.Entities;
using StoryBooks.Libraries.Validation;

using System;
using System.Diagnostics.CodeAnalysis;

using static StoryBooks.Features.Authors.Domain.AuthorDomainConstants;

public class Book : Entity<string>
{
    private readonly HashSet<Genre> genres;
    private readonly HashSet<Tag> tags;
    private readonly HashSet<Stat> stats;
    private readonly HashSet<Chapter> chapters;

    internal Book(string title, BookCover cover, string? description, Author author)
    {
        Validate(title, description);

        this.Title = title;
        this.Cover = cover;
        this.Description = description;
        this.Status = BookStatus.Draft;
        this.Author = author;

        this.genres = new HashSet<Genre>();
        this.tags = new HashSet<Tag>();
        this.stats = new HashSet<Stat>();
        this.chapters = new HashSet<Chapter>();
    }

    [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "For EF")]
    private Book(string title, string? description)
    {
        this.Title = title;
        this.Description = description;

        this.Cover = default!;
        this.Status = default!;
        this.Author = default!;

        this.genres = new HashSet<Genre>();
        this.tags = new HashSet<Tag>();
        this.stats = new HashSet<Stat>();
        this.chapters = new HashSet<Chapter>();
    }

    public string Title { get; private set; }

    public BookCover Cover { get; private set; }

    public BookStatus Status { get; private set; }

    public Author Author { get; private set; }

    public string? Description { get; private set; }

    public Chapter? FirstChapter => this.chapters.FirstOrDefault(c => c.IsFirstChapter);

    public IReadOnlyCollection<Genre> Genres => this.genres.ToList().AsReadOnly();

    public IReadOnlyCollection<Tag> Tags => this.tags.ToList().AsReadOnly();

    public IReadOnlyCollection<Stat> Stats => this.stats.ToList().AsReadOnly();

    public IReadOnlyCollection<Chapter> Chapters => this.chapters.ToList().AsReadOnly();

    internal Book AddChapter(
        string title,
        string content,
        bool isCheckpoint = false,
        bool isDiceChoice = false)
    {
        bool isFirstChapter = !this.chapters.Any();
        this.chapters.Add(
            new Chapter(title, content, this, isFirstChapter, isCheckpoint, isDiceChoice));

        return this;
    }

    internal Book RemoveChapter(int chapterId)
    {
        var chapter = this.chapters.FirstOrDefault(c => c.Id == chapterId);
        if (chapter == null)
        {
            throw new ChapterNotFoundException();
        }

        return RemoveChapter(chapter);
    }

    internal Book RemoveChapter(Chapter chapter)
    {
        this.chapters.Remove(chapter);
        return this;
    }

    internal Book SetFirstChapter(int chapterId)
    {
        var currentFirst = this.chapters.FirstOrDefault(c => c.IsFirstChapter);
        var newFirst = this.chapters.FirstOrDefault(c => c.Id == chapterId);
        if (newFirst == null)
        {
            throw new ChapterNotFoundException();
        }

        currentFirst?.UnsetAsFirstChapter();
        newFirst.SetAsFirstChapter();
        return this;
    }

    internal Book UpdateTitle(string title)
    {
        ValidateTitle(title);

        this.Title = title;
        return this;
    }

    internal Book UpdateCover(string imageName, byte[] imageContent)
    {
        this.Cover = new BookCover(imageName, imageContent);
        return this;
    }

    internal Book Publish()
    {
        this.Status = BookStatus.Published;
        return this;
    }

    internal Book Archive()
    {
        this.Status = BookStatus.Archived;
        return this;
    }

    internal Book UpdateDescription(string? description)
    {
        ValidateDescription(description);
        this.Description = description;
        return this;
    }

    internal Book AddTag(string tagName)
    {
        this.tags.Add(new Tag(tagName));
        return this;
    }

    internal Book AddTag(Tag tag)
    {
        this.tags.Add(tag);
        return this;
    }

    internal Book AddTags(IEnumerable<Tag> tags)
    {
        this.tags.AddRange(tags);
        return this;
    }

    internal Book RemoveTag(int tagId)
    {
        var tag = this.tags.FirstOrDefault(c => c.Id == tagId);
        if (tag == null)
        {
            throw new TagNotFoundException();
        }

        return RemoveTag(tag);
    }

    internal Book RemoveTag(Tag tag)
    {
        this.tags.Remove(tag);
        return this;
    }

    internal Book ClearTags()
    {
        this.tags.Clear();
        return this;
    }

    internal Book AddGenre(string genreName)
    {
        this.genres.Add(new Genre(genreName));
        return this;
    }

    internal Book AddGenre(Genre genre)
    {
        this.genres.Add(genre);
        return this;
    }

    internal Book AddGenres(IEnumerable<Genre> genres)
    {
        this.genres.AddRange(genres);
        return this;
    }

    internal Book RemoveGenre(int genreId)
    {
        var genre = this.genres.FirstOrDefault(c => c.Id == genreId);
        if (genre == null)
        {
            throw new GenreNotFoundException();
        }

        return RemoveGenre(genre);
    }

    internal Book RemoveGenre(Genre genre)
    {
        this.genres.Remove(genre);
        return this;
    }

    internal Book ClearGenres()
    {
        this.genres.Clear();
        return this;
    }

    internal Book AddStat(
        string name,
        decimal value,
        bool isCritical,
        decimal minValue,
        string groupName)
    {
        this.stats.Add(new Stat(name, value, isCritical, minValue, groupName, this));
        return this;
    }

    internal Book RemoveStat(Stat stat)
    {
        this.stats.Remove(stat);
        return this;
    }

    private static void Validate(string title, string? description)
    {
        ValidateTitle(title);
        ValidateDescription(description);
    }

    private static void ValidateDescription(string? description)
    {
        if (description == null)
        {
            return;
        }

        Guard.ForMaxStringLength<InvalidBookException>(
            value: description, 
            maxLength: MaxBookDescriptionLength, 
            name: nameof(description));
    }

    private static void ValidateTitle(string title)
    {
        Guard.ForStringLength<InvalidBookException>(
            value: title,
            minLength: MinBookTitleLength,
            maxLength: MaxBookTitleLength,
            name: nameof(title));
    }
}