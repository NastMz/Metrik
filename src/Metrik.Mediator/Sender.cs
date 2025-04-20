using Metrik.Mediator.Interfaces;
using Metrik.Mediator.Internal;
using System.Collections.Concurrent;

namespace Metrik.Mediator
{
    /// <summary>
    /// Mediator sender implementation.
    /// </summary>
    internal sealed class Sender : ISender
    {
        /// <summary>
        /// Factory to create single instances of handlers.
        /// </summary>
        private readonly Func<Type, object?> _singleInstanceFactory;

        /// <summary>
        /// Factory to create multiple instances of handlers.
        /// </summary>
        private readonly Func<Type, IEnumerable<object>> _multiInstanceFactory;

        /// <summary>
        /// Cache for request handlers.
        /// </summary>
        private static readonly ConcurrentDictionary<Type, RequestHandlerBase> _requestHandlers = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="Sender"/> class.
        /// </summary>
        /// <param name="singleInstanceFactory">The factory to create single instances of handlers.</param>
        /// <param name="multiInstanceFactory"></param>
        public Sender(Func<Type, object?> singleInstanceFactory, Func<Type, IEnumerable<object>> multiInstanceFactory)
        {
            _singleInstanceFactory = singleInstanceFactory;
            _multiInstanceFactory = multiInstanceFactory;
        }

        /// <inheritdoc />
        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return Send(request, typeof(TResponse), cancellationToken)
                .ContinueWith(t => (TResponse)t.Result!, cancellationToken);
        }

        /// <inheritdoc />
        public Task<object?> Send(object request, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);

            var requestType = request.GetType();
            var responseType = GetResponseType(requestType);

            return Send(request, responseType, cancellationToken);
        }

        /// <inheritdoc />
        public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
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

        /// <inheritdoc />
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
                var handlerInterfaceType = typeof(IRequestHandler<,>).MakeGenericType(requestType, responseType);
                var handler = _singleInstanceFactory(handlerInterfaceType) ?? throw new InvalidOperationException(
                        $"No handler registered for {requestType.Name} with response type {responseType.Name}");

                var behaviorsType = typeof(IPipelineBehavior<,>).MakeGenericType(requestType, responseType);
                var behaviors = _multiInstanceFactory(behaviorsType);

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
