namespace StoryBooks.Features.Domain.Entities;

using StoryBooks.Features.Domain.Events;

public interface IEntity
{
    IReadOnlyCollection<IDomainEvent> Events { get; }

    void ClearEvents();

    void AddEvent(IDomainEvent domainEvent);
}