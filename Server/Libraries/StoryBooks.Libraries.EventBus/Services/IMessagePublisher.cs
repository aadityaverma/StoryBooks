namespace StoryBooks.Libraries.EventBus.Services;

using System;
using System.Threading.Tasks;

public interface IMessagePublisher
{
    Task Publish<TMessage>(TMessage message);

    Task Publish<TMessage>(TMessage message, CancellationToken cancellationToken);

    Task Publish<TMessage>(TMessage message, Type messageType);
}
