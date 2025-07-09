using Metrik.Domain.Abstractions.Models;
using Nast.SimpleMediator.Abstractions;

namespace Metrik.Application.Abstractions.Interfaces.Messaging
{
    /// <summary>
    /// Defines a handler for a query that produces a response.
    /// </summary>
    /// <typeparam name="TQuery">Type of query</typeparam>
    /// <typeparam name="TResponse">Type of response</typeparam>
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse>
    {
    }
}
