namespace StoryBooks.Features.Authors.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using StoryBooks.Features.Authors.Domain.Entities;

using static StoryBooks.Features.Authors.Domain.AuthorDomainConstants;
using static StoryBooks.Features.Authors.Infrastructure.InfrastructureConstants;

internal class BattleConfigurations : IEntityTypeConfiguration<Battle>
{
    public void Configure(EntityTypeBuilder<Battle> builder)
    {
        builder.ToTable($"{TablesPrefix}_{nameof(Battle)}");

        builder
            .HasKey(b => b.Id);

        builder
            .Property(b => b.EnemyName)
            .IsRequired()
            .HasMaxLength(MaxBattleNameLength);

        var manyToMany = builder
            .HasMany(b => b.IncludedStats)
            .WithMany(s => s.Battles);

        manyToMany
            .UsingEntity(f => f.ToTable($"{TablesPrefix}_{nameof(Battle)}{nameof(Stat)}"));

        manyToMany.LeftNavigation
            .SetField("includedStats");
    }
}