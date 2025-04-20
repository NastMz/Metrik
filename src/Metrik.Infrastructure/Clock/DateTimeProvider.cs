using Metrik.Application.Abstractions.Interfaces.Clock;

namespace Metrik.Infrastructure.Clock
{
    /// <summary>
    /// Provides the current UTC date and time.
    /// </summary>
    internal sealed class DateTimeProvider : IDateTimeProvider
    {
        /// <inheritdoc />
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
