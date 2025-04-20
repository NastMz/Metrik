namespace Metrik.Mediator.Interfaces
{
    /// <summary>
    /// Defines a mediator to encapsulate requests and send notifications.
    /// </summary>
    public interface IMediator
    {
        /// <summary>
        /// Sends a request to the single handler.
        /// </summary>
        /// <typeparam name="TResponse">Type of the expected response</typeparam>
        /// <param name="request">Request to send</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns>Response from the handler</returns>
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends a request to the single handler (untyped version).
        /// </summary>
        /// <param name="request">Request to send</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns>Response from the handler, if any</returns>
        Task<object?> Send(object request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Publishes a notification to all registered handlers.
        /// </summary>
        /// <param name="notification">Notification to publish</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        Task Publish(object notification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Publishes a notification to all registered handlers.
        /// </summary>
        /// <typeparam name="TNotification">Type of the notification</typeparam>
        /// <param name="notification">Notification to publish</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
            where TNotification : INotification;

        /// <summary>
        /// Creates a stream for a stream request.
        /// </summary>
        /// <typeparam name="TResponse">Type of the stream elements</typeparam>
        /// <param name="request">Stream request</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns>Stream of responses</returns>
        IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a stream for a stream request (untyped version).
        /// </summary>
        /// <param name="request">Stream request</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns>Stream of responses</returns>
        IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default);
    }
}
