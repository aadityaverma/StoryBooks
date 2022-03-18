namespace StoryBooks.Features.Authors.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

using StoryBooks.Features.Authors.Domain.Entities;

public interface IAuthorDbContext
{
    DbSet<Author> Authors { get; }

    DbSet<Book> Books { get; }

    DbSet<BookCover> BookCovers { get; }

    DbSet<Genre> Genres { get; }

    DbSet<Tag> Tags { get; }

    DbSet<Chapter> Chapters { get; }

    DbSet<Stat> Stats { get; }

    DbSet<Battle> Battles { get; }

    DbSet<Effect> Effects { get; }

    DbSet<StatModifier> StatChanges { get; }

    DbSet<Choice> Choices { get; }
}