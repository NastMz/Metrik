namespace Metrik.Application.Exceptions
{
    /// <summary>
    /// Represents an exception that occurs when there is a concurrency conflict.
    /// </summary>
    public sealed class ConcurrencyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrencyException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public ConcurrencyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
