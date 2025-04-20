namespace Metrik.Application.Abstractions.Interfaces.Mediator
{
    /// <summary>
    /// Defines a mediator for sending requests to a single handler.
    /// </summary>
    public interface IMediator
    {
        /// <summary>
        /// Sends a request to a single handler and returns a response.
        /// </summary>
        /// <typeparam name="TResponse">Type of the expected response</typeparam>
        /// <param name="request">Request to send</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns>Response from the request</returns>
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a request without expecting a response.
        /// </summary>
        /// <param name="request">Request to send</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        Task Send(IRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends an object as a request, determining the handler type at runtime.
        /// </summary>
        /// <param name="request">Request to send</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns>Response from the request</returns>
        Task<object> Send(object request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Publishes a notification to all registered handlers.
        /// </summary>
        /// <param name="notification">Notification to publish</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        Task Publish(object notification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Publishes a typed notification to all registered handlers.
        /// </summary>
        /// <typeparam name="TNotification">Type of the notification</typeparam>
        /// <param name="notification">Notification to publish</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification;
    }
}
