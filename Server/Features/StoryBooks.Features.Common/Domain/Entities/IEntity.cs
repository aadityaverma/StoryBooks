namespace StoryBooks.Features.Common.Domain.Entities;

using StoryBooks.Features.Common.Domain.Events;

public interface IEntity
{
    IReadOnlyCollection<IDomainEvent> Events { get; }

    void ClearEvents();

    void AddEvent(IDomainEvent domainEvent);
}