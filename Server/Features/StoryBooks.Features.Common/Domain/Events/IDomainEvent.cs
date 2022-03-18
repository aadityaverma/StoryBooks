namespace StoryBooks.Features.Common.Domain.Events;

using MediatR;

using System;

public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
}