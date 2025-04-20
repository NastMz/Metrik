using Metrik.Domain.Abstractions.Models;
using Metrik.Domain.Entities.Transactions.Enums;
using Metrik.Domain.Shared.ValueObjects;

namespace Metrik.Domain.Entities.Transactions.Events
{
    /// <summary>
    /// Domain event that is raised when a transaction is created.
    /// </summary>
    /// <param name="TransactionId">The unique identifier for the transaction.</param>
    /// <param name="AccountId">The unique identifier for the account associated with the transaction.</param>
    /// <param name="Amount">The amount of money involved in the transaction.</param>
    /// <param name="Type">The type of the transaction (e.g., income or expense).</param>
    public sealed record TransactionCreatedDomainEvent(
        Guid TransactionId,
        Guid AccountId,
        Money Amount,
        TransactionType Type
    ) : DomainEvent;
}
