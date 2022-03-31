namespace StoryBooks.Libraries.EventBus.Services;

using MassTransit;

using Microsoft.Extensions.Hosting;

using System.Threading;
using System.Threading.Tasks;

public class MessageWorker : BackgroundService
{
    private readonly IBus bus;

    public MessageWorker(IBus bus)
    {
        this.bus = bus;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken) 
        => this.bus.Publish(new { Test = "test" }, cancellationToken: stoppingToken);
}

