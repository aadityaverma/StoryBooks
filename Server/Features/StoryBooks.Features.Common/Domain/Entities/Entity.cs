namespace StoryBooks.Features.Common.Domain.Entities;

using System.Collections.Generic;
using System.Linq;

using Events;

using StoryBooks.Features.Common.Domain.Exceptions;
using StoryBooks.Libraries.Validation;

using static StoryBooks.Libraries.Validation.CommonValidationConstants;

public abstract class Entity<TKey> : IEntity
{
    private readonly ICollection<IDomainEvent> events;

    protected Entity()
    {
        this.Id = default!;
        this.events = new List<IDomainEvent>();
    }

    public virtual TKey Id { get; protected set; } = default!;

    public IReadOnlyCollection<IDomainEvent> Events => this.events.ToList();

    public void ClearEvents() => this.events.Clear();

    public bool IsTransient()
    {
        if (this.Id is null)
        {
            return true;
        }

        return this.Id.Equals(default(TKey));
    }

    public void AddEvent(IDomainEvent domainEvent)
        => this.events.Add(domainEvent);

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TKey> other)
        {
            return false;
        }

        if (other.Id is null || this.Id is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (this.GetType() != other.GetType())
        {
            return false;
        }

        if (this.Id.Equals(default) || other.Id.Equals(default))
        {
            return false;
        }

        return this.Id.Equals(other.Id);
    }

    public static bool operator ==(Entity<TKey>? first, Entity<TKey>? second)
    {
        if (first is null && second is null)
        {
            return true;
        }

        if (first is null || second is null)
        {
            return false;
        }

        return first.Equals(second);
    }

    public static bool operator !=(Entity<TKey>? first, Entity<TKey>? second) => !(first == second);

    public override int GetHashCode() => (this.GetType().ToString() + this.Id).GetHashCode();

    protected virtual void ValidateGuidId(string id)
    {
        Guard.ForMaxStringLength<InvalidEntityException>(id, MaxGuidLength, InvalidGuidLength);
    }
}