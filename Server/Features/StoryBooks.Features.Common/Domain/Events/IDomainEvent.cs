namespace StoryBooks.Features.Common.Domain.Events
{
    using System;

    public interface IDomainEvent
    {
        DateTime OccurredOn { get; }
    }
}
