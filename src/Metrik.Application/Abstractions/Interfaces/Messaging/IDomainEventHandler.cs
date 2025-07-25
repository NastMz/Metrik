﻿using Metrik.Application.Abstractions.Models;
using Metrik.Domain.Abstractions.Interfaces;
using Nast.SimpleMediator.Abstractions;

namespace Metrik.Application.Abstractions.Interfaces.Messaging
{
    /// <summary>
    /// Defines a handler for domain events.
    /// </summary>
    /// <typeparam name="TDomainEvent">Type of domain event</typeparam>
    public interface IDomainEventHandler<TDomainEvent> : INotificationHandler<DomainEventNotification<TDomainEvent>>
        where TDomainEvent : IDomainEvent
    {
    }
}
