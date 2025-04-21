using Metrik.Domain.Abstractions.Models;

namespace Metrik.Application.Errors
{
    public static class CommonErrors
    {
        /// <summary>
        /// Error indicating that a resource was not found.
        /// </summary>
        public static readonly Error NotFound = new(
            "Common.NotFound",
            "The requested resource was not found.",
            ErrorType.NotFound,
            "Errors.Common.NotFound"
        );

        /// <summary>
        /// Error indicating that an internal server error occurred.
        /// </summary>
        public static readonly Error InternalServerError = new(
            "Common.InternalServerError",
            "An internal server error occurred.",
            ErrorType.Failure,
            "Errors.Common.InternalServer"
        );

        public static readonly Error ValidationError = new(
            "Common.ValidationError",
            "One or more validation errors occurred.",
            ErrorType.Validation,
            "Errors.Common.ValidationError"
        );

        public static readonly Error Unauthorized = new(
            "Common.Unauthorized",
            "You are not authorized to perform this action.",
            ErrorType.Unauthorized,
            "Errors.Common.Unauthorized"
        );

        public static readonly Error Forbidden = new(
            "Common.Forbidden",
            "You do not have permission to access this resource.",
            ErrorType.Forbidden,
            "Errors.Common.Forbidden"
        );

        public static readonly Error InvalidId = new(
            "Common.InvalidId",
            "The provided identifier is invalid.",
            ErrorType.Validation,
            "Errors.Common.InvalidId"
        );

        public static readonly Error NullValue = new(
            "Common.NullValue",
            "A required value was null.",
            ErrorType.Validation,
            "Errors.Common.NullValue"
        );
    }
}
