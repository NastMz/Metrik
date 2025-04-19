using Metrik.Domain.Abstractions.Interfaces;

namespace Metrik.Domain.Entities.Users.Events
{
    /// <summary>
    /// Domain event that is raised when a user is created.
    /// </summary>
    /// <param name="UserId"></param>
    public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;
}
