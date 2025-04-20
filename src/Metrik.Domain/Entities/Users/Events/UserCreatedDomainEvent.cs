using Metrik.Domain.Abstractions.Models;

namespace Metrik.Domain.Entities.Users.Events
{
    /// <summary>
    /// Domain event that is raised when a user is created.
    /// </summary>
    /// <param name="UserId"></param>
    public sealed record UserCreatedDomainEvent(Guid UserId) : DomainEvent;
}
