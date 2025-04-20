namespace Metrik.Application.Abstractions.Interfaces.Mediator
{
    /// <summary>
    /// Defines a pipeline behavior for requests.
    /// </summary>
    /// <typeparam name="TRequest">Type of the request</typeparam>
    /// <typeparam name="TResponse">Type of the expected response</typeparam>
    public interface IPipelineBehavior<in TRequest, TResponse> where TRequest : notnull
    {
        /// <summary>
        /// Pipeline for the request flow.
        /// </summary>
        /// <param name="request">Request to process</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <param name="next">Delegate for the next handler in the pipeline</param>
        /// <returns>Response from the request</returns>
        Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Delegate for the next handler in the pipeline.
    /// </summary>
    /// <typeparam name="TResponse">Type of the expected response</typeparam>
    /// <returns>Response from the request</returns>
    public delegate Task<TResponse> RequestHandlerDelegate<TResponse>();
}
