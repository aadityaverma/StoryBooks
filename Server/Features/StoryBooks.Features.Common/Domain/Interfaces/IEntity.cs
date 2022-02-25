namespace StoryBooks.Features.Common.Domain.Interfaces
{
    using Events;

    public interface IEntity
    {
        IReadOnlyCollection<IDomainEvent> Events { get; }

        void ClearEvents();
    }
}
