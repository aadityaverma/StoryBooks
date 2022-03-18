namespace StoryBooks.Features.Authors.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using StoryBooks.Features.Authors.Domain.Entities;

using static StoryBooks.Features.Authors.Domain.AuthorDomainConstants;
using static StoryBooks.Features.Authors.Infrastructure.InfrastructureConstants;

internal class EffectConfigurations : IEntityTypeConfiguration<Effect>
{
    public void Configure(EntityTypeBuilder<Effect> builder)
    {
        builder.ToTable($"{TablesPrefix}_{nameof(Effect)}");

        builder
            .HasKey(e => e.Id);

        builder
            .Property(e => e.Text)
            .IsRequired()
            .HasMaxLength(MaxEffectTextLength);

        builder
            .HasOne(e => e.Chapter)
            .WithMany(c => c.Effects)
            .IsRequired();

        builder
            .HasMany(e => e.StatModifiers)
            .WithOne()
            .Metadata
            .PrincipalToDependent?
            .SetField("modifiers");
    }
}
