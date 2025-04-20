namespace Metrik.Application.Abstractions.Interfaces.Mediator
{
    /// <summary>
    /// Defines a handler for a request that produces a response
    /// </summary>
    /// <typeparam name="TRequest">Type of request</typeparam>
    /// <typeparam name="TResponse">Type of response</typeparam>
    public interface IRequestHandler<in TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Handles the request and produces a response
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response of the request</returns>
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Defines a handler for a request that does not produce a response (void)
    /// </summary>
    /// <typeparam name="TRequest">Type of request</typeparam>
    public interface IRequestHandler<in TRequest> : IRequestHandler<TRequest, Unit>
        where TRequest : IRequest<Unit>
    {
    }
}
