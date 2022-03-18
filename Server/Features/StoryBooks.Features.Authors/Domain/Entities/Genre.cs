namespace StoryBooks.Features.Authors.Domain.Entities;

using StoryBooks.Features.Authors.Domain.Exceptions;
using StoryBooks.Features.Domain.Entities;
using StoryBooks.Libraries.Validation;

using static StoryBooks.Features.Authors.Domain.AuthorDomainConstants;

public class Genre : Entity<int>
{
    private readonly HashSet<Book> books;

    internal Genre(string name)
    {
        ValidateName(name);

        this.Name = name;

        this.books = new HashSet<Book>();
    }

    public string Name { get; private set; }

    public IReadOnlyCollection<Book> Books => this.books.ToList().AsReadOnly();

    internal Genre UpdateName(string name)
    {
        ValidateName(name);

        this.Name = name;
        return this;
    }

    private static void ValidateName(string name)
    {
        Guard.ForStringLength<InvalidGenreException>(
            value: name,
            minLength: MinGenreNameLength,
            maxLength: MaxGenreNameLength,
            name: nameof(name));
    }
}