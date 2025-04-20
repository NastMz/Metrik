using Metrik.Mediator.Interfaces;

namespace Metrik.Mediator
{
    /// <summary>
    /// Extensiones para el mediador
    /// </summary>
    public static class MediatorExtensions
    {
        /// <summary>
        /// Envía una solicitud con respuesta vacía
        /// </summary>
        /// <param name="mediator">El mediador</param>
        /// <param name="request">La solicitud</param>
        /// <param name="cancellationToken">Token de cancelación opcional</param>
        /// <returns>Task que representa la operación</returns>
        public static Task Send(
            this IMediator mediator,
            IRequest request,
            CancellationToken cancellationToken = default)
        {
            return mediator.Send(request, cancellationToken);
        }

        /// <summary>
        /// Publica múltiples notificaciones secuencialmente
        /// </summary>
        /// <param name="mediator">El mediador</param>
        /// <param name="notifications">Las notificaciones a publicar</param>
        /// <param name="cancellationToken">Token de cancelación opcional</param>
        /// <returns>Task que representa la operación</returns>
        public static async Task PublishAll(
            this IMediator mediator,
            IEnumerable<INotification> notifications,
            CancellationToken cancellationToken = default)
        {
            foreach (var notification in notifications)
            {
                await mediator.Publish(notification, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
