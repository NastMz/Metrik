using Metrik.Domain.Abstractions.Models;
using Metrik.Mediator.Interfaces;

namespace Metrik.Application.Abstractions.Interfaces.Messaging
{
    /// <summary>
    /// Represents a command that does not return a response.
    /// </summary>
    public interface ICommand : IRequest<Result>, IBaseCommand
    {
    }

    /// <summary>
    /// Represents a command that returns a response.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
    {
    }

    /// <summary>
    /// Represents a base command interface. Used for adding pipeline behaviors and other cross-cutting concerns.
    /// </summary>
    public interface IBaseCommand
    {
    }
}
