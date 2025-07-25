﻿using Metrik.Domain.Abstractions.Models;
using Metrik.Domain.Shared.ValueObjects;

namespace Metrik.Domain.Entities.Accounts.Events
{
    /// <summary>
    /// Represents a domain event that is raised when an account is updated.
    /// </summary>
    /// <param name="AccountId">The unique identifier for the account.</param>
    /// <param name="PreviusBalance">The previous balance of the account.</param>
    /// <param name="NewBalance">The updated balance of the account.</param>
    public record AccountUpdatedDomainEvent(
        Guid AccountId,
        Money PreviusBalance,
        Money NewBalance
    ) : DomainEvent;
}
