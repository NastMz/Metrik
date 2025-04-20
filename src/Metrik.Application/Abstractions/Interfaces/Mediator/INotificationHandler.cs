namespace Metrik.Application.Abstractions.Interfaces.Mediator
{
    /// <summary>
    /// Defines a handler for a notification.
    /// </summary>
    /// <typeparam name="TNotification">Type of the notification</typeparam>
    public interface INotificationHandler<in TNotification>
        where TNotification : INotification
    {
        /// <summary>
        /// Handles a notification.
        /// </summary>
        /// <param name="notification">Notification to handle</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        Task Handle(TNotification notification, CancellationToken cancellationToken);
    }
}
