using Metrik.Application.Abstractions.Interfaces.Messaging;
using Metrik.Mediator.Interfaces;
using Microsoft.Extensions.Logging;

namespace Metrik.Application.Abstractions.Behaviors
{
    /// <summary>
    /// Logging behavior for commands.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseCommand
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingBehavior{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="logger">The logger instance used for logging.</param>
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Handles the logging behavior for commands.
        /// </summary>
        /// <param name="request">The request to process.</param>
        /// <param name="next">The delegate for the next handler in the pipeline.</param>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation, with the response as the result if successful.</returns>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var name = request.GetType().Name;

            try
            {
                _logger.LogInformation("Executing command: {Command}", name);

                var response = await next();

                _logger.LogInformation("Command {Command} executed successfully", name);

                return response;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Command {Command} processing failed", name);

                throw;
            }
        }
    }
}
