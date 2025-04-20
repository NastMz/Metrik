using Metrik.Mediator.Interfaces;

namespace Metrik.Mediator
{
    /// <summary>
    /// Extensions for the publisher
    /// </summary>
    public static class PublisherExtensions
    {
        /// <summary>
        /// Publish multiple notifications sequentially
        /// </summary>
        /// <param name="publisher">The publisher instance</param>
        /// <param name="notifications">The notifications to publish</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A task representing the publish operation</returns>
        public static async Task PublishAll(
            this IPublisher publisher,
            IEnumerable<INotification> notifications,
            CancellationToken cancellationToken = default)
        {
            foreach (var notification in notifications)
            {
                await publisher.Publish(notification, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
