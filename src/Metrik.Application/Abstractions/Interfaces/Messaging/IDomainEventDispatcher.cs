using Metrik.Domain.Abstractions.Interfaces;

namespace Metrik.Application.Abstractions.Interfaces.Messaging
{
    /// <summary>
    /// Defines a service for dispatching domain events.
    /// </summary>
    public interface IDomainEventDispatcher
    {
        /// <summary>
        /// Dispatches a collection of domain events asynchronously.
        /// </summary>
        Task DispatchEventsAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
    }
}
