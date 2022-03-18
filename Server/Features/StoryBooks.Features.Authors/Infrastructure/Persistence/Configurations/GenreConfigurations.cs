namespace StoryBooks.Features.Authors.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using StoryBooks.Features.Authors.Domain.Entities;

using static StoryBooks.Features.Authors.Domain.AuthorDomainConstants;
using static StoryBooks.Features.Authors.Infrastructure.InfrastructureConstants;

internal class GenreConfigurations : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable($"{TablesPrefix}_{nameof(Genre)}");

        builder
            .HasKey(g => g.Id);

        builder
            .Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(MaxGenreNameLength);

        var manyToMany = builder
            .HasMany(g => g.Books)
            .WithMany(b => b.Genres);

        manyToMany.UsingEntity(f => f.ToTable($"{TablesPrefix}_{nameof(Book)}{nameof(Genre)}"));

        manyToMany.LeftNavigation
            .SetField("books");
    }
}