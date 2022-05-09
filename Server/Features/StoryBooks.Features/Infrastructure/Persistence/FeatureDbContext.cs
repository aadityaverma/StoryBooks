namespace StoryBooks.Features.Infrastructure.Persistence;

using MediatR;

using Microsoft.EntityFrameworkCore;

using StoryBooks.Features.Domain.Entities;

public abstract class FeatureDbContext : DbContext
{
    protected readonly IMediator mediator;
    private readonly Stack<object> savesChangesTracker;

    public FeatureDbContext(
        DbContextOptions options,
        IMediator mediator)
        : base(options)
    {
        this.mediator = mediator;
        this.savesChangesTracker = new Stack<object>();
    }

    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        this.savesChangesTracker.Push(new object());

        var entities = this.ChangeTracker
            .Entries<IEntity>()
            .Select(e => e.Entity)
            .Where(e => e.Events.Any())
            .ToArray();

        foreach (var entity in entities)
        {
            var events = entity.Events.ToArray();
            entity.ClearEvents();

            foreach (var domainEvent in events)
            {
                await this.mediator.Publish(domainEvent, cancellationToken);
            }
        }

        this.savesChangesTracker.Pop();
        if (!this.savesChangesTracker.Any())
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        return 0;
    }
}