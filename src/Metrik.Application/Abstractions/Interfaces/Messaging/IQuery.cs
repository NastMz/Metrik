using Metrik.Application.Abstractions.Interfaces.Mediator;
using Metrik.Domain.Abstractions.Models;

namespace Metrik.Application.Abstractions.Interfaces.Messaging
{
    /// <summary>
    /// Represents a query that returns a response.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
