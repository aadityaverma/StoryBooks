namespace StoryBooks.Features.Common.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using StoryBooks.Features.Common.Domain.Entities;

using static StoryBooks.Features.Common.Domain.DomainConstants;

public static class ImageConfigurations
{
    public static void Configure<T>(EntityTypeBuilder<T> builder)
        where T : Image
    {
        builder
            .HasKey(c => c.Id);

        builder
            .Property(c => c.Content)
            .IsRequired();

        builder
            .Property(c => c.Name)
            .HasMaxLength(MaxImageNameLength)
            .IsRequired();
    }
}