namespace Metrik.Application.Abstractions.Interfaces.Clock
{
    /// <summary>
    /// Provides an abstraction for getting the UTC date and time.
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Gets the current UTC date and time.
        /// </summary>
        DateTime UtcNow { get; }
    }
}
