using Metrik.Domain.Abstractions.Interfaces;
using Metrik.Domain.Shared.ValueObjects;

namespace Metrik.Domain.Entities.Accounts.Events
{
    /// <summary>
    /// Represents a domain event that is raised when an account is updated.
    /// </summary>
    /// <param name="AccountId">The unique identifier for the account.</param>
    /// <param name="NewBalance">The updated balance of the account.</param>
    public record AccountUpdatedDomainEvent(Guid AccountId, Money NewBalance) : IDomainEvent;
}
