namespace Metrik.Domain.Abstractions.Interfaces
{
    /// <summary>
    /// Represents a domain event independent of any technical implementation.
    /// </summary>
    public interface IDomainEvent
    {
        /// <summary>
        /// The moment when the event occurred.
        /// </summary>
        DateTime OccurredOn { get; }

        /// <summary>
        /// Unique identifier for the event.
        /// </summary>
        Guid Id { get; }
    }
}
