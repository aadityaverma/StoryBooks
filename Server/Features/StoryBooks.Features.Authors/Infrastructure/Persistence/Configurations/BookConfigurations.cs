namespace StoryBooks.Features.Authors.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using StoryBooks.Features.Authors.Domain.Entities;

using static StoryBooks.Libraries.Validation.CommonValidationConstants;
using static StoryBooks.Features.Authors.Domain.AuthorDomainConstants;
using static StoryBooks.Features.Authors.Infrastructure.InfrastructureConstants;

internal class BookConfigurations : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable($"{TablesPrefix}_{nameof(Book)}");

        builder
            .HasKey(b => b.Id);

        builder
            .Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(MaxBookTitleLength);

        builder
            .HasOne(b => b.Cover)
            .WithMany()
            .HasForeignKey("CoverId")
            .OnDelete(DeleteBehavior.Restrict);

        builder.OwnsOne(b => b.Status, s =>
        {
            s.WithOwner();
            s.Property(e => e.Value)
             .IsRequired();
        });

        builder
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey("AuthorId")
            .IsRequired();

        builder
            .Property(b => b.Description)
            .HasMaxLength(MaxBookDescriptionLength);

        builder
            .HasMany(b => b.Genres)
            .WithMany(g => g.Books)
            .LeftNavigation
            .SetField("genres");

        builder
            .HasMany(b => b.Tags)
            .WithMany(t => t.Books)
            .LeftNavigation
            .SetField("tags");

        builder
            .HasMany(b => b.Stats)
            .WithOne(s => s.Book)
            .Metadata
            .PrincipalToDependent?
            .SetField("stats");
    }
}