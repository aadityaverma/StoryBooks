namespace StoryBooks.Features.Common.Domain.Interfaces;

public interface IFactory<out TEntity>
    where TEntity : IAggregateRoot
{
    TEntity Build();

    void Reset();
}

public interface IFactoryWithBuilder<out TEntity>
    where TEntity : IAggregateRoot
{
    TEntity Build(string builderId);

    void Reset();
}