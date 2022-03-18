namespace StoryBooks.Features.Identity.Infrastructure.Persistence.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using StoryBooks.Features.Identity.Domain.Entities;

using static StoryBooks.Features.Identity.Domain.IdentityDomainConstants;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(Validation.MaxNameLength);

        builder
            .Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(Validation.MaxNameLength);
    }
}