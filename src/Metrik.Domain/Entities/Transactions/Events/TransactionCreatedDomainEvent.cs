using Metrik.Domain.Abstractions.Interfaces;

namespace Metrik.Domain.Entities.Transactions.Events
{
    /// <summary>
    /// Domain event that is raised when a transaction is created.
    /// </summary>
    /// <param name="TransactionId">The unique identifier for the transaction.</param>
    public sealed record TransactionCreatedDomainEvent(Guid TransactionId) : IDomainEvent;
}
