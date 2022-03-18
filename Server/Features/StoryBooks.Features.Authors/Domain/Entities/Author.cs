namespace StoryBooks.Features.Authors.Domain.Entities;

using StoryBooks.Features.Authors.Domain.Exceptions;
using StoryBooks.Features.Domain.Entities;
using StoryBooks.Features.Domain.Interfaces;
using StoryBooks.Libraries.Validation;

using System.Diagnostics.CodeAnalysis;

using static StoryBooks.Features.Authors.Domain.AuthorDomainConstants;

public class Author : Entity<string>, IAggregateRoot
{
    private readonly HashSet<Book> books;

    internal Author(string userId, string name)
    {
        Validate(userId, name);

        this.UserId = userId;
        this.Name = name;
        this.Alias = name;

        this.books = new HashSet<Book>();
    }

    [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "For EF")]
    private Author(string userId, string name, string alias, string description)
    {
        UserId = userId;
        Name = name;
        Alias = alias;
        Description = description;

        books = new HashSet<Book>();
    }

    public string UserId { get; private set; }

    public string Name { get; private set; }

    public string Alias { get; private set; }

    public string? Description { get; private set; }

    public IReadOnlyCollection<Book> Books => this.books.ToList().AsReadOnly();

    internal Author UpdateName(string name, string? alias = null)
    {
        ValidateName(name, nameof(name));
        ValidateName(alias ?? name, nameof(alias));

        this.Name = name;
        this.Alias = alias ?? name;
        return this;
    }

    internal Author UpdateDescription(string? description)
    {
        this.Description = description;
        return this;
    }

    internal Author StartBook(string title, BookCover cover, string description)
    {
        this.books.Add(new Book(title, cover, description, this));
        return this;
    }

    internal Author RemoveBook(string bookId)
    {
        var book = this.books.FirstOrDefault(book => book.Id == bookId);
        if (book == null)
        {
            throw new BookNotFoundException($"Book with id {bookId} is not found!");
        }

        this.RemoveBook(book);
        return this;
    }

    internal Author RemoveBook(Book book)
    {
        this.books.Remove(book);
        return this;
    }

    private void Validate(string userId, string name)
    {
        ValidateGuidId(userId);
        ValidateName(name, nameof(name));
    }

    private static void ValidateName(string name, string propName)
    {
        Guard.ForStringLength<InvalidAuthorException>(
            value: name,
            minLength: MinAuthorNameLength,
            maxLength: MaxAuthorNameLength,
            name: propName);
    }
}