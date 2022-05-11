namespace StoryBooks.Features.Authors.Application.Consumers;

using StoryBooks.Features.Application.Messages;

public class BecomeAuthorConsumer : IConsumer<BecomeAuthorMessage>
{
    public BecomeAuthorConsumer()
    {

    }

    public Task Consume(ConsumeContext<BecomeAuthorMessage> context) 
        => Task.CompletedTask;
}
