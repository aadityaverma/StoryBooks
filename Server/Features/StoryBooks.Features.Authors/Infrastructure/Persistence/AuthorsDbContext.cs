namespace StoryBooks.Features.Authors.Infrastructure.Persistence;

using MediatR;

using Microsoft.EntityFrameworkCore;

using StoryBooks.Features.Authors.Domain.Entities;
using StoryBooks.Features.Common.Infrastructure.Persistence;

public class AuthorsDbContext : FeatureDbContext, IAuthorDbContext
{
    public AuthorsDbContext(
        DbContextOptions<AuthorsDbContext> options,
        IMediator mediator)
        : base(options, mediator)
    {
    }

    public DbSet<Author> Authors { get; set; } = default!;

    public DbSet<Book> Books { get; set; } = default!;

    public DbSet<BookCover> BookCovers { get; set; } = default!;

    public DbSet<Genre> Genres { get; set; } = default!;

    public DbSet<Tag> Tags { get; set; } = default!;

    public DbSet<Chapter> Chapters { get; set; } = default!;

    public DbSet<Stat> Stats { get; set; } = default!;

    public DbSet<Battle> Battles { get; set; } = default!;

    public DbSet<Effect> Effects { get; set; } = default!;

    public DbSet<StatModifier> StatChanges { get; set; } = default!;

    public DbSet<Choice> Choices { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthorsDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
