namespace Metrik.Domain.Abstractions.Models
{
    /// <summary>
    /// Enumeration representing the type of error that occurred.
    /// </summary>
    public enum ErrorType
    {
        Validation,
        NotFound,
        Conflict,
        Unauthorized,
        Forbidden,
        Failure
    }

    /// <summary>
    /// Represents something that went wrong in the system.
    /// </summary>
    /// <param name="Code">The error code.</param>
    /// <param name="Name">The error name.</param>
    /// <param name="LocalizationKey">Optional key for localization lookup.</param>
    public record Error(string Code, string Name, ErrorType Type, string? LocalizationKey = null)
    {
        /// <summary>
        /// Gets the localization key for this error. Falls back to Code if not explicitly set.
        /// </summary>
        public string GetLocalizationKey() => LocalizationKey ?? Code;

        /// <summary>
        /// Represents a successful operation with no error.
        /// </summary>
        public static Error None = new(string.Empty, string.Empty, ErrorType.Validation);

        /// <summary>
        /// Represents an error that occurs when a null value is provided where it is not allowed.
        /// </summary>
        public static Error NullValue = new(
            "Error.NullValue",
            "Null value was provided",
            ErrorType.Validation,
            "Errors.Common.NullValue");
    }
}