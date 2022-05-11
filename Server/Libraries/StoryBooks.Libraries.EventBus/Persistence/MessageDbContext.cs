namespace StoryBooks.Libraries.EventBus.Persistence;

using Microsoft.EntityFrameworkCore;

using Models;

using System.Reflection;

public abstract class MessageDbContext : DbContext
{
    protected MessageDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Message> Messages { get; set; } = default!;

    protected abstract Assembly ConfigurationsAssembly { get; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new MessageConfiguration());
        base.OnModelCreating(builder);
    }
}
