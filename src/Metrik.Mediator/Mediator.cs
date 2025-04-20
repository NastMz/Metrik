using Metrik.Mediator.Interfaces;
using Metrik.Mediator.Internal;
using System.Collections.Concurrent;

namespace Metrik.Mediator
{
    // <summary>
    /// Implementation of the IMediator interface.
    /// </summary>
    public class Mediator : IMediator
    {
        /// <summary>
        /// Factory for resolving single instances of services.
        /// </summary>
        private readonly Func<Type, object?> _singleInstanceFactory;

        /// <summary>
        /// Factory for resolving multiple instances of services.
        /// </summary>
        private readonly Func<Type, IEnumerable<object>> _multiInstanceFactory;

        /// <summary>
        /// Cache for request handlers to avoid multiple activations.
        /// </summary>
        private static readonly ConcurrentDictionary<Type, RequestHandlerBase> _requestHandlers = new();

        /// <summary>
        /// Initializes a new instance of the Mediator class with a factory for resolving services.
        /// </summary>
        /// <param name="serviceFactory">Factory for resolving services</param>
        public Mediator(Func<Type, object?> serviceFactory)
            : this(serviceFactory, t => serviceFactory(t) as IEnumerable<object> ?? [])
        {
        }

        /// <summary>
        /// Initializes a new instance of the Mediator class with separate factories for single and multiple instances.
        /// </summary>
        public Mediator(Func<Type, object?> singleInstanceFactory, Func<Type, IEnumerable<object>> multiInstanceFactory)
        {
            _singleInstanceFactory = singleInstanceFactory;
            _multiInstanceFactory = multiInstanceFactory;
        }

        /// <summary>
        /// Sends a request and returns the response.
        /// </summary>
        /// <typeparam name="TResponse">Type of the expected response</typeparam>
        /// <param name="request">The request to send</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The response from the handler</returns>
        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return Send(request, typeof(TResponse), cancellationToken)
                .ContinueWith(t => (TResponse)t.Result!, cancellationToken);
        }

        /// <summary>
        /// Sends a request and returns the response (untyped version).
        /// </summary>
        /// <param name="request">The request to send</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The response from the handler, if any</returns>
        /// <exception cref="ArgumentNullException">If the request is null</exception>
        public Task<object?> Send(object request, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);

            var requestType = request.GetType();
            var responseType = GetResponseType(requestType);

            return Send(request, responseType, cancellationToken);
        }

        /// <summary>
        /// Sends a request and returns the response (private method).
        /// </summary>
        /// <param name="request">The request to send</param>
        /// <param name="responseType">Type of the expected response</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The response from the handler</returns>
        private Task<object?> Send(object request, Type responseType, CancellationToken cancellationToken)
        {
            var requestType = request.GetType();
            var handler = GetHandler(requestType, responseType);

            return handler.Handle(request, cancellationToken);
        }

        /// <summary>
        /// Gets the request handler for the specified request and response types.
        /// </summary>
        /// <param name="requestType">Type of the request</param>
        /// <param name="responseType">Type of the response</param>
        /// <returns>The request handler</returns>
        /// <exception cref="InvalidOperationException">If no handler is registered for the request type</exception>
        private RequestHandlerBase GetHandler(Type requestType, Type responseType)
        {
            var handlerType = typeof(RequestHandlerWrapper<,>).MakeGenericType(requestType, responseType);
            var key = requestType;

            return _requestHandlers.GetOrAdd(key, _ =>
            {
                // Get request handler type
                var handlerInterfaceType = typeof(IRequestHandler<,>).MakeGenericType(requestType, responseType);
                var handler = _singleInstanceFactory(handlerInterfaceType) ?? throw new InvalidOperationException(
                        $"No handler registered for {requestType.Name} with response type {responseType.Name}");

                // Get behavior types
                var behaviorsType = typeof(IPipelineBehavior<,>).MakeGenericType(requestType, responseType);
                var behaviors = _multiInstanceFactory(behaviorsType);

                // Create wrapper
                return (RequestHandlerBase)Activator.CreateInstance(
                    handlerType,
                    handler,
                    behaviors)!;
            });
        }

        /// <summary>
        /// Gets the response type from the request type.
        /// </summary>
        /// <param name="requestType">Type of the request</param>
        /// <returns>The response type</returns>
        /// <exception cref="InvalidOperationException">If the request type does not implement IRequest<TResponse></exception>
        private static Type GetResponseType(Type requestType)
        {
            var requestInterface = requestType.GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>));

            return requestInterface == null
                ? throw new InvalidOperationException($"{requestType.Name} does not implement IRequest<TResponse>")
                : requestInterface.GetGenericArguments()[0];
        }

        /// <summary>
        /// Publishes a notification to all registered handlers.
        /// </summary>
        /// <typeparam name="TNotification">Type of the notification</typeparam>
        /// <param name="notification">The notification to publish</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The task representing the asynchronous operation</returns>
        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
            where TNotification : INotification
        {
            return Publish((object)notification, cancellationToken);
        }

        /// <summary>
        /// Publishes a notification to all registered handlers (untyped version).
        /// </summary>
        /// <param name="notification">The notification to publish</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The task representing the asynchronous operation</returns>
        /// <exception cref="ArgumentNullException">If the notification is null</exception>
        public Task Publish(object notification, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(notification);

            var notificationType = notification.GetType();
            var handlerType = typeof(INotificationHandler<>).MakeGenericType(notificationType);
            var handlers = _multiInstanceFactory(handlerType);

            var handlerTasks = new List<Task>();

            foreach (var handler in handlers)
            {
                var wrapperType = typeof(NotificationHandlerWrapper<>).MakeGenericType(notificationType);
                var wrapper = (dynamic)Activator.CreateInstance(wrapperType, handler)!;
                var task = wrapper.Handle((dynamic)notification, cancellationToken);
                handlerTasks.Add(task);
            }

            return Task.WhenAll(handlerTasks);
        }

        /// <summary>
        /// Creates a stream of responses for a given request.
        /// </summary>
        /// <typeparam name="TResponse">Type of the response</typeparam>
        /// <param name="request">The request to send</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The stream of responses</returns>
        /// <exception cref="ArgumentNullException">If the request is null</exception>
        /// <exception cref="InvalidOperationException">If no stream handler is registered for the request type</exception>
        public IAsyncEnumerable<TResponse> CreateStream<TResponse>(
            IStreamRequest<TResponse> request,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);

            var requestType = request.GetType();
            var responseType = typeof(TResponse);
            var handlerType = typeof(IStreamRequestHandler<,>).MakeGenericType(requestType, responseType);
            var handler = _singleInstanceFactory(handlerType);

            return handler == null
                ? throw new InvalidOperationException($"No stream handler registered for {requestType.Name}")
                : (IAsyncEnumerable<TResponse>)((dynamic)handler).Handle((dynamic)request, cancellationToken);
        }

        /// <summary>
        /// Creates a stream of responses for a given request (untyped version).
        /// </summary>
        /// <param name="request">The request to send</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The stream of responses</returns>
        /// <exception cref="ArgumentNullException">If the request is null</exception>
        /// <exception cref="InvalidOperationException">If no stream handler is registered for the request type</exception>
        public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);

            var requestType = request.GetType();
            var responseType = GetStreamResponseType(requestType);
            var handlerType = typeof(IStreamRequestHandler<,>).MakeGenericType(requestType, responseType);
            var handler = _singleInstanceFactory(handlerType);

            return handler == null
                ? throw new InvalidOperationException($"No stream handler registered for {requestType.Name}")
                : (IAsyncEnumerable<object?>)((dynamic)handler).Handle((dynamic)request, cancellationToken).Cast<object?>();
        }

        /// <summary>
        /// Gets the response type from the stream request type.
        /// </summary>
        /// <param name="requestType">Type of the request</param>
        /// <returns>The response type</returns>
        /// <exception cref="InvalidOperationException">If the request type does not implement IStreamRequest<TResponse></exception>
        private static Type GetStreamResponseType(Type requestType)
        {
            var requestInterface = requestType.GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IStreamRequest<>));

            return requestInterface == null
                ? throw new InvalidOperationException($"{requestType.Name} does not implement IStreamRequest<T>")
                : requestInterface.GetGenericArguments()[0];
        }
    }
}
