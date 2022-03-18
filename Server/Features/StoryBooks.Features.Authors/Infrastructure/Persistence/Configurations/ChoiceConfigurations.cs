namespace StoryBooks.Features.Authors.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using StoryBooks.Features.Authors.Domain.Entities;

using static StoryBooks.Features.Authors.Domain.AuthorDomainConstants;
using static StoryBooks.Features.Authors.Infrastructure.InfrastructureConstants;

internal class ChoiceConfigurations : IEntityTypeConfiguration<Choice>
{
    public void Configure(EntityTypeBuilder<Choice> builder)
    {
        builder.ToTable($"{TablesPrefix}_{nameof(Choice)}");

        builder
            .HasKey(c => c.Id);

        builder
            .Property(c => c.Text)
            .IsRequired()
            .HasMaxLength(MaxChoiceTextLength);

        builder
            .HasOne(c => c.NextChapter)
            .WithMany(c => c.Choices)
            .IsRequired();
    }
}