using Metrik.Domain.Abstractions.Interfaces;
using Metrik.Mediator.Interfaces;

namespace Metrik.Application.Abstractions.Models
{
    /// <summary>
    /// Represents a notification for a domain event.
    /// </summary>
    /// <typeparam name="TDomainEvent">The type of the domain event.</typeparam>
    public class DomainEventNotification<TDomainEvent> : INotification
        where TDomainEvent : IDomainEvent
    {
        /// <summary>
        /// The domain event associated with this notification.
        /// </summary>
        public TDomainEvent DomainEvent { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEventNotification{TDomainEvent}"/> class.
        /// </summary>
        /// <param name="domainEvent">The domain event to be associated with this notification.</param>
        public DomainEventNotification(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }
    }
}
