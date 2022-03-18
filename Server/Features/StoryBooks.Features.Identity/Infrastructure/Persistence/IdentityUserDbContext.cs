namespace StoryBooks.Features.Identity.Infrastructure.Persistence;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using StoryBooks.Features.Identity.Domain.Entities;

public class IdentityUserDbContext : IdentityDbContext<User>
{
    public IdentityUserDbContext(DbContextOptions<IdentityUserDbContext> options) : base(options)
    {
    }

    protected IdentityUserDbContext(DbContextOptions options)
    : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityUserDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
