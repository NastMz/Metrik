using Metrik.Domain.Abstractions.Interfaces;

namespace Metrik.Domain.Abstractions.Models
{
    /// <summary>
    /// Base class for domain events.
    /// </summary>
    public abstract record DomainEvent : IDomainEvent
    {
        /// <summary>
        /// Unique identifier for the event.
        /// </summary>
        public Guid Id { get; } = Guid.NewGuid();

        /// <summary>
        /// The moment when the event occurred.
        /// </summary>
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
