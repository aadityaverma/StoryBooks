namespace StoryBooks.Features.Authors.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using StoryBooks.Features.Authors.Domain.Entities;

using static StoryBooks.Features.Authors.Domain.AuthorDomainConstants;

internal class StatConfigurations : IEntityTypeConfiguration<Stat>
{
    public void Configure(EntityTypeBuilder<Stat> builder)
    {
        builder.ToTable($"Authors_{nameof(Stat)}");

        builder
            .HasKey(s => s.Id);

        builder
            .Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(MaxStatNameLength);

        builder
            .Property(s => s.Value)
            .HasPrecision(18, 2)
            .IsRequired();

        builder
            .Property(s => s.MinimumValue)
            .HasPrecision(18, 2)
            .IsRequired();

        builder
            .Property(s => s.GroupName)
            .IsRequired()
            .HasMaxLength(MaxStatNameLength);

        builder
            .HasOne(s => s.Book)
            .WithMany(b => b.Stats)
            .HasForeignKey("BookId")
            .IsRequired();
    }
}