namespace StoryBooks.Features.Authors.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using StoryBooks.Features.Authors.Domain.Entities;

using static StoryBooks.Features.Authors.Domain.AuthorDomainConstants;
using static StoryBooks.Features.Authors.Infrastructure.InfrastructureConstants;

internal class ChapterConfigurations : IEntityTypeConfiguration<Chapter>
{
    public void Configure(EntityTypeBuilder<Chapter> builder)
    {
        builder.ToTable($"{TablesPrefix}_{nameof(Chapter)}");

        builder
            .HasKey(c => c.Id);

        builder
            .Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(MaxChapterTitleLength);

        builder
            .Property(c => c.Content)
            .IsRequired();

        builder
            .HasOne(c => c.Book)
            .WithMany(b => b.Chapters)
            .IsRequired();

        builder
            .HasOne(c => c.Battle)
            .WithMany()
            .HasForeignKey("BattleId");

        builder
            .HasMany(c => c.Effects)
            .WithOne(e => e.Chapter)
            .Metadata
            .PrincipalToDependent?
            .SetField("effects");

        builder
            .HasMany(c => c.Choices)
            .WithOne(c => c.NextChapter)
            .Metadata
            .PrincipalToDependent?
            .SetField("choices");

    }
}