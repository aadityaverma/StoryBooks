namespace StoryBooks.Features.Common.Infrastructure.Persistence;

using StoryBooks.Features.Common.Domain.Interfaces;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public abstract class DataRepository<TDbContext, TEntity> : IDomainRepository<TEntity>
    where TDbContext : IDbContext
    where TEntity : class, IAggregateRoot
{
    protected DataRepository(TDbContext db) => this.Data = db;

    protected TDbContext Data { get; }

    public virtual IQueryable<TEntity> All() => this.Data.Set<TEntity>();

    public virtual async Task<TEntity> Add(TEntity entity, CancellationToken cancellationToken = default)
    {
        await this.Data.Set<TEntity>().AddAsync(entity, cancellationToken);
        await this.Save(cancellationToken);
        return entity;
    }

    public virtual async Task<bool> Delete(TEntity entity, CancellationToken cancellationToken = default)
    {
        this.Data.Set<TEntity>().Remove(entity);
        await this.Save(cancellationToken);
        return true;
    }

    public virtual async Task<int> Save(CancellationToken cancellationToken = default)
    {
        return await this.Data.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task Save(TEntity entity, CancellationToken cancellationToken = default)
    {
        this.Data.Update(entity);
        await this.Data.SaveChangesAsync(cancellationToken);
    }
}