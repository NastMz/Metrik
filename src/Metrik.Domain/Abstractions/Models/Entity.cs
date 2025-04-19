using Metrik.Domain.Abstractions.Interfaces;

namespace Metrik.Domain.Abstractions.Models
{
    /// <summary>
    /// Base class for all entities.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// List of domain events that have occurred on the entity.
        /// </summary>
        private readonly List<IDomainEvent> _domainEvents = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier for the entity.</param>
        protected Entity(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// The unique identifier for the entity.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Date and time when the entity was created.
        /// </summary>
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        /// <summary>
        /// Date and time when the entity was last updated.
        /// </summary>
        public DateTime UpdatedAt { get; internal set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets all the domain events that have occurred on the entity.
        /// </summary>
        /// <returns>The list of domain events.</returns>
        public IReadOnlyList<IDomainEvent> GetDomainEvents()
        {

            return [.. _domainEvents];
        }

        /// <summary>
        /// Clears all the domain events that have occurred on the entity.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        /// <summary>
        /// Raises a domain event for the entity.
        /// </summary>
        /// <param name="domainEvent">The domain event to raise.</param>
        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
