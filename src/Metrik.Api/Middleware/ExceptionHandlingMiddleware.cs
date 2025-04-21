
using Metrik.Application.Abstractions.Interfaces.Localization;
using Metrik.Application.Errors;
using Metrik.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Metrik.Api.Middleware
{
    /// <summary>
    /// Middleware for handling exceptions globally in the application.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        /// <summary>
        /// The next middleware in the pipeline.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// The logger for logging exceptions.
        /// </summary>
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        /// <summary>
        /// The localization service for localizing exception messages.
        /// </summary>
        private readonly ILocalizationService _localizationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlingMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="logger">The logger for logging exceptions.</param>
        /// <param name="localizationService">The localization service for localizing exception messages.</param>
        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger,
            ILocalizationService localizationService)
        {
            _next = next;
            _logger = logger;
            _localizationService = localizationService;
        }

        /// <summary>
        /// Invokes the middleware to handle exceptions.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while processing request: {Exception}", ex.Message);

                var exceprionDetails = GetExceptionDetails(ex);

                var problemDetails = new ProblemDetails
                {
                    Status = exceprionDetails.Status,
                    Type = exceprionDetails.Type,
                    Detail = exceprionDetails.Detail,
                };

                if (exceprionDetails.Errors != null)
                {
                    problemDetails.Extensions.Add("errors", exceprionDetails.Errors);
                }

                context.Response.StatusCode = exceprionDetails.Status;

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }

        /// <summary>
        /// Gets the exception details based on the type of exception.
        /// </summary>
        /// <param name="ex">The exception to get details for.</param>
        /// <returns>The exception details.</returns>
        private ExceptionDetails GetExceptionDetails(Exception ex)
        {
            return ex switch
            {
                ValidationException validationException => new ExceptionDetails(
                    StatusCodes.Status400BadRequest,
                    "ValidationError",
                    _localizationService.GetLocalizedString(
                        CommonErrors.ValidationError.GetLocalizationKey(),
                        CommonErrors.ValidationError.Name
                    ),
                    validationException.Errors
                ),
                _ => new ExceptionDetails(
                    StatusCodes.Status500InternalServerError,
                    "InternalServerError",
                    _localizationService.GetLocalizedString(
                        CommonErrors.InternalServerError.GetLocalizationKey(),
                        CommonErrors.InternalServerError.Name
                    ),
                    null
                )
            };
        }

        internal record ExceptionDetails(
            int Status,
            string Type,
            string Detail,
            IEnumerable<object>? Errors
        );
    }
}
