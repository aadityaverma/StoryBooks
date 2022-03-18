namespace StoryBooks.Features.Common.Domain.Events;

using MediatR;

using System.Threading.Tasks;

public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
