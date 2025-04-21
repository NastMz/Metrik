using Metrik.Application.Abstractions.Interfaces.Localization;
using Metrik.Domain.Abstractions.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Metrik.Application.Extensions
{
    /// <summary>
    /// Extension methods for the <see cref="Error"/> class.
    /// </summary>
    public static class ResultExtensions
    {
        /// <summary>
        /// Converts a failed result into an appropriate HTTP response.
        /// </summary>
        public static IActionResult ToActionResult(
            this Result result,
            ILocalizationService localizationService,
            ControllerBase controller)
        {
            var errorResponse = new ApiErrorResponse(
                result.Error.Code,
                result.Error.GetLocalizedMessage(localizationService)
            );

            return result.Error.Type switch
            {
                ErrorType.NotFound => controller.NotFound(errorResponse),
                ErrorType.Validation => controller.BadRequest(errorResponse),
                ErrorType.Conflict => controller.Conflict(errorResponse),
                ErrorType.Unauthorized => controller.StatusCode(StatusCodes.Status401Unauthorized, errorResponse),
                ErrorType.Forbidden => controller.StatusCode(StatusCodes.Status403Forbidden, errorResponse),
                _ => controller.StatusCode(StatusCodes.Status500InternalServerError, errorResponse)
            };
        }

        /// <summary>
        /// Converts a result into an appropriate HTTP response.
        /// </summary>
        public static IActionResult ToActionResult<T>(
            this Result<T> result,
            ILocalizationService localizationService,
            ControllerBase controller)
        {
            return result.IsSuccess
                ? controller.Ok(result.Value)
                : ToActionResult((Result)result, localizationService, controller);
        }
    }

    /// <summary>
    /// Represents an error response for API requests.
    /// </summary>
    /// <param name="Code">The error code.</param>
    /// <param name="Message">The error message.</param>
    public record ApiErrorResponse(string Code, string Message);
}
