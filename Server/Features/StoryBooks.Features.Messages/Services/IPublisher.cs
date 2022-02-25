namespace StoryBooks.Features.Messages.Services
{
    using System;
    using System.Threading.Tasks;

    public interface IPublisher
    {
        Task Publish<TMessage>(TMessage message);

        Task Publish<TMessage>(TMessage message, Type messageType);
    }
}
