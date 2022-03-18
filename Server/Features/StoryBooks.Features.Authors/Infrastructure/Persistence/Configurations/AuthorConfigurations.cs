namespace StoryBooks.Features.Authors.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using StoryBooks.Features.Authors.Domain.Entities;

using static StoryBooks.Features.Authors.Domain.AuthorDomainConstants;
using static StoryBooks.Features.Authors.Infrastructure.InfrastructureConstants;
using static StoryBooks.Libraries.Validation.CommonValidationConstants;

internal class AuthorConfigurations : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable($"{TablesPrefix}_{nameof(Author)}");

        builder
            .HasKey(c => c.Id);

        builder
            .Property(c => c.UserId)
            .IsRequired()
            .HasMaxLength(MaxGuidLength);

        builder
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(MaxAuthorNameLength);

        builder
            .Property(c => c.Alias)
            .IsRequired()
            .HasMaxLength(MaxAuthorNameLength);

        builder
            .HasMany(c => c.Books)
            .WithOne(b => b.Author)
            .HasForeignKey("AuthorId")
            .OnDelete(DeleteBehavior.Restrict);
    }
}