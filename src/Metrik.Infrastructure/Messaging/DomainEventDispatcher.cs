using Metrik.Application.Abstractions.Interfaces.Messaging;
using Metrik.Application.Abstractions.Models;
using Metrik.Domain.Abstractions.Interfaces;
using Metrik.Mediator.Interfaces;
using Microsoft.Extensions.Logging;

namespace Metrik.Infrastructure.Messaging
{
    /// <summary>
    /// Implements the IDomainEventDispatcher interface to dispatch domain events.
    /// </summary>
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        /// <summary>
        /// The mediator used to publish domain events.
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// The logger used for logging domain event dispatching information.
        /// </summary>
        private readonly ILogger<DomainEventDispatcher> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEventDispatcher"/> class.
        /// </summary>
        /// <param name="mediator">The mediator used to publish domain events.</param>
        /// <param name="logger">The logger used for logging domain event dispatching information.</param>
        public DomainEventDispatcher(IMediator mediator, ILogger<DomainEventDispatcher> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task DispatchEventsAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
        {
            foreach (var domainEvent in domainEvents)
            {
                _logger.LogInformation("Dispatching domain event: {Event} ({Id})",
                    domainEvent.GetType().Name, domainEvent.Id);

                try
                {
                    var domainEventType = domainEvent.GetType();
                    var notificationType = typeof(DomainEventNotification<>).MakeGenericType(domainEventType);
                    var notification = Activator.CreateInstance(notificationType, domainEvent);

                    if (notification != null)
                    {
                        await _mediator.Publish(notification, cancellationToken);
                    }
                    else
                    {
                        _logger.LogWarning("Failed to create notification for domain event: {Event} ({Id})",
                            domainEvent.GetType().Name, domainEvent.Id);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error dispatching domain event: {Event} ({Id})",
                        domainEvent.GetType().Name, domainEvent.Id);

                    throw;
                }
            }
        }
    }
}
