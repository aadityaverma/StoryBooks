namespace StoryBooks.Libraries.EventBus.Services;

using MassTransit;

using System;
using System.Threading;
using System.Threading.Tasks;

public class Publisher : IPublisher
{
    private const int TimeoutMilliseconds = 2000;

    private readonly IBus bus;

    public Publisher(IBus bus) => this.bus = bus;

    public Task Publish<TMessage>(TMessage message)
        => this.bus.Publish(message  ?? default!, GetCancellationToken());

    public Task Publish<TMessage>(TMessage message, Type messageType)
        => this.bus.Publish(message ?? default!, messageType, GetCancellationToken());

    private static CancellationToken GetCancellationToken()
    {
        var timeout = TimeSpan.FromMilliseconds(TimeoutMilliseconds);
        var cancellationTokenSource = new CancellationTokenSource(timeout);
        return cancellationTokenSource.Token;
    }
}
