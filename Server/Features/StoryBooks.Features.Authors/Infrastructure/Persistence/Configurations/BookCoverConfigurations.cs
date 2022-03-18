namespace StoryBooks.Features.Authors.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using StoryBooks.Features.Authors.Domain.Entities;
using StoryBooks.Features.Infrastructure.Persistence.Configurations;

using static StoryBooks.Features.Authors.Infrastructure.InfrastructureConstants;

internal class BookCoverConfigurations : IEntityTypeConfiguration<BookCover>
{
    public void Configure(EntityTypeBuilder<BookCover> builder)
    {
        builder.ToTable($"{TablesPrefix}_{nameof(BookCover)}");

        ImageConfigurations.Configure(builder);
    }
}