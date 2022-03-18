namespace StoryBooks.Features.Authors.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using StoryBooks.Features.Authors.Domain.Entities;

using static StoryBooks.Features.Authors.Infrastructure.InfrastructureConstants;

internal class StatModifierConfigurations : IEntityTypeConfiguration<StatModifier>
{
    public void Configure(EntityTypeBuilder<StatModifier> builder)
    {
        builder.ToTable($"{TablesPrefix}_{nameof(StatModifier)}");

        builder
            .HasKey(s => s.Id);

        builder
            .Property(s => s.Change)
            .HasPrecision(18, 2)
            .IsRequired();

        builder
            .HasOne(s => s.Stat)
            .WithMany()
            .IsRequired();
    }
}