using Metrik.Application.Abstractions.Interfaces.Mediator;
using Metrik.Domain.Abstractions.Models;

namespace Metrik.Application.Abstractions.Interfaces.Messaging
{
    /// <summary>
    /// Defines a handler for a command that does not produce a response.
    /// </summary>
    /// <typeparam name="TCommand">Type of command</typeparam>
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
        where TCommand : ICommand
    {
    }

    /// <summary>
    /// Defines a handler for a command that produces a response.
    /// </summary>
    /// <typeparam name="TCommand">Type of command</typeparam>
    /// <typeparam name="TResponse">Type of response</typeparam>
    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
        where TCommand : ICommand<TResponse>
    {
    }
}
