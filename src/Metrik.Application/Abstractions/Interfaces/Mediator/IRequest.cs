namespace Metrik.Application.Abstractions.Interfaces.Mediator
{
    /// <summary>
    /// Marks a class as a request that produces a response.
    /// </summary>
    /// <typeparam name="TResponse">Type of the response</typeparam>
    public interface IRequest<out TResponse> { }

    /// <summary>
    /// Marks a class as a request that produces no response.
    /// </summary>
    public interface IRequest : IRequest<Unit> { }
}
