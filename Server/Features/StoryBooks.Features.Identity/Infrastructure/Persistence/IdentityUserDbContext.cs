namespace StoryBooks.Features.Identity.Infrastructure.Persistence;

using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using StoryBooks.Features.Identity.Domain.Entities;

public class IdentityUserDbContext : IdentityDbContext<User>, IDataProtectionKeyContext
{
    public IdentityUserDbContext(DbContextOptions<IdentityUserDbContext> options) : base(options)
    {
    }

    protected IdentityUserDbContext(DbContextOptions options)
    : base(options)
    {
    }

    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityUserDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
