namespace Metrik.Domain.Shared.ValueObjects
{
    /// <summary>
    /// Represents a monetary amount with a specific currency.
    /// </summary>
    /// <param name="Amount">The amount of money.</param>
    /// <param name="Currency">The currency of the money.</param>
    public record Money(decimal Amount, Currency Currency)
    {
        /// <summary>
        /// Allows addition of two Money objects.
        /// </summary>
        /// <param name="first">The first Money object.</param>
        /// <param name="second">The second Money object.</param>
        /// <returns>The sum of the two Money objects.</returns>
        /// <exception cref="InvalidOperationException">If the currencies of the two Money objects are different.</exception>
        public static Money operator +(Money first, Money second)
        {
            if (first.Currency != second.Currency)
                throw new InvalidOperationException("Cannot add money with different currencies.");

            return new Money(first.Amount + second.Amount, first.Currency);
        }

        /// <summary>
        /// Allows subtraction of two Money objects.
        /// </summary>
        /// <param name="first">The first Money object.</param>
        /// <param name="second">The second Money object.</param>
        /// <returns>The difference of the two Money objects.</returns>
        /// <exception cref="InvalidOperationException">If the currencies of the two Money objects are different.</exception>
        public static Money operator -(Money first, Money second)
        {
            if (first.Currency != second.Currency)
                throw new InvalidOperationException("Cannot subtract money with different currencies.");

            return new Money(first.Amount - second.Amount, first.Currency);
        }

        /// <summary>
        /// Gets an empty Money object with zero amount and no currency.
        /// </summary>
        /// <returns>The empty Money object.</returns>
        public static Money Zero() => new(0, Currency.None);

        /// <summary>
        /// Gets an empty Money object with zero amount and the specified currency.
        /// </summary>
        /// <param name="currency">The currency of the Money object.</param>
        /// <returns>The empty Money object.</returns>
        public static Money Zero(Currency currency) => new(0, currency);

        /// <summary>
        /// Checks if the Money object is a zero money object.
        /// </summary>
        /// <returns>True if the Money object is zero; otherwise, false.</returns>
        public bool IsZero() => this == Zero(Currency);
    }
}
