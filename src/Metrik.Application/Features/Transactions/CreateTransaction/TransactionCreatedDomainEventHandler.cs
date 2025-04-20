using Metrik.Application.Abstractions.Interfaces.Messaging;
using Metrik.Application.Abstractions.Models;
using Metrik.Domain.Entities.Transactions.Events;
using Microsoft.Extensions.Logging;

namespace Metrik.Application.Features.Transactions.CreateTransaction
{
    /// <summary>
    /// Handles the creation of a new transaction.
    /// </summary>
    internal sealed class TransactionCreatedDomainEventHandler : IDomainEventHandler<TransactionCreatedDomainEvent>
    {
        private readonly ILogger<TransactionCreatedDomainEventHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionCreatedDomainEventHandler"/> class.
        /// </summary>
        /// <param name="logger">The logger instance to be used for logging.</param>
        public TransactionCreatedDomainEventHandler(ILogger<TransactionCreatedDomainEventHandler> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Handles the domain event when a transaction is created.
        /// </summary>
        /// <param name="notification">The domain event notification containing the transaction created event.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task Handle(DomainEventNotification<TransactionCreatedDomainEvent> notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Transaction created: {TransactionId}, AccountId: {AccountId}, Amount: {Amount}, Type: {Type}",
                notification.DomainEvent.TransactionId,
                notification.DomainEvent.AccountId,
                notification.DomainEvent.Amount,
                notification.DomainEvent.Type
            );

            // Here can add any additional logic that needs to be executed when a transaction is created. (e.g., sending notifications, updating caches, etc.)

            return Task.CompletedTask;
        }
    }
}
