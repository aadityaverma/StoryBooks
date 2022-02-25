namespace StoryBooks.Features.Common.Domain.Interfaces
{
    public interface IDomainRepository<TEntity> where TEntity : IAggregateRoot
    {
        IQueryable<TEntity> All();

        Task<TEntity> Add(TEntity entity, CancellationToken cancellationToken = default);

        Task<bool> Delete(TEntity entity, CancellationToken cancellationToken = default);

        Task<int> Save(CancellationToken cancellationToken = default);
    }
}
