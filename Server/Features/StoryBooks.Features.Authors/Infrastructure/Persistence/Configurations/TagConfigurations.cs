﻿namespace StoryBooks.Features.Authors.Infrastructure.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using StoryBooks.Features.Authors.Domain.Entities;

    using static StoryBooks.Features.Authors.Domain.AuthorDomainConstants;
    using static StoryBooks.Features.Authors.Infrastructure.InfrastructureConstants;

    internal class TagConfigurations : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable($"{TablesPrefix}_{nameof(Tag)}");

            builder
                .HasKey(g => g.Id);

            builder
                .Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(MaxGenreNameLength);

            builder
                .HasMany(g => g.Books)
                .WithMany(b => b.Tags)
                .LeftNavigation
                .SetField("books");
        }
    }
}
