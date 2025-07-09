using FluentValidation;
using Metrik.Application.Abstractions.Interfaces.Messaging;
using Metrik.Application.Exceptions;
using Nast.SimpleMediator.Abstractions;

namespace Metrik.Application.Abstractions.Behaviors
{
    /// <summary>
    /// Validation behavior for commands.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseCommand
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationBehavior{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="validators">The collection of validators to use for validation.</param>
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /// <summary>
        /// Handles the validation behavior for commands.
        /// </summary>
        /// <param name="request">The request to process.</param>
        /// <param name="next">The delegate for the next handler in the pipeline.</param>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation, with the response as the result if successful.</returns>
        /// <exception cref="ValidationException">Thrown when validation fails.</exception>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);

            var validationResults = _validators
                .Select(v => v.Validate(context))
                .Where(r => r.Errors.Count > 0)
                .SelectMany(r => r.Errors)
                .Select(e => new ValidationError(e.PropertyName.ToLowerInvariant(), e.ErrorMessage))
                .ToList();

            if (validationResults.Count != 0)
            {
                throw new Exceptions.ValidationException(validationResults);
            }

            return await next();
        }
    }
}