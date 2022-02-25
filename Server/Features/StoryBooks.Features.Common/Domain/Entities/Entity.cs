namespace StoryBooks.Features.Common.Domain.Entities
{
    using System.Collections.Generic;
    using System.Linq;

    using Events;

    public abstract class Entity<TKey>
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
            if (this.Id == null)
            {
                return true;
            }

            return this.Id.Equals(default(TKey));
        }

        protected void AddEvent(IDomainEvent domainEvent)
            => this.events.Add(domainEvent);

        public override bool Equals(object? obj)
        {
            if (obj is not Entity<TKey> other)
            {
                return false;
            }

            if (other.Id == null || this.Id == null)
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
    }
}