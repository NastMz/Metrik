namespace Metrik.Domain.Abstractions.Models
{
    /// <summary>
    /// Represents something that went wrong in the system.
    /// </summary>
    /// <param name="Code">The error code.</param>
    /// <param name="Name">The error name.</param>
    public record Error(string Code, string Name)
    {
        /// <summary>
        /// Represents a successful operation with no error.
        /// </summary>
        public static Error None = new(string.Empty, string.Empty);

        /// <summary>
        /// Represents an error that occurs when a null value is provided where it is not allowed.
        /// </summary>
        public static Error NullValue = new("Error.NullValue", "Null value was provided");
    }
}
