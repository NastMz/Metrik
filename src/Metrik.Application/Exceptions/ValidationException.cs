namespace Metrik.Application.Exceptions
{
    /// <summary>
    /// Represents an exception that occurs during validation.
    /// </summary>
    public sealed class ValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="errors">The collection of validation errors.</param>
        public ValidationException(IEnumerable<ValidationError> errors)
            : base("Validation failed")
        {
            Errors = errors;
        }

        /// <summary>
        /// Collection of validation errors.
        /// </summary>
        public IEnumerable<ValidationError> Errors { get; }
    }
}
