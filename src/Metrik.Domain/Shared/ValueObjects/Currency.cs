namespace Metrik.Domain.Shared.ValueObjects
{
    /// <summary>
    /// Represents a currency.
    /// </summary>
    public record Currency
    {
        internal static readonly Currency None = new("");
        public static readonly Currency Usd = new("USD");
        public static readonly Currency Cop = new("COP");
        public static readonly Currency Eur = new("EUR");

        /// <summary>
        /// Initializes a new instance of the <see cref="Currency"/> class with the specified currency code.
        /// </summary>
        /// <param name="code">The currency code.</param>
        public Currency(string code) => Code = code;

        /// <summary>
        /// The currency code.
        /// </summary>
        public string Code { get; init; }

        /// <summary>
        /// Gets the currency symbol for the specified currency code.
        /// </summary>
        /// <param name="code">The currency code.</param>
        /// <returns>The currency symbol if found.</returns>
        /// <exception cref="ApplicationException">When the currency code is not found.</exception>
        public static Currency FromCode(string code)
        {
            return All.FirstOrDefault(c => c.Code == code)
                   ?? throw new ApplicationException($"Currency with code '{code}' not found.");
        }

        /// <summary>
        /// Gets all available currencies.
        /// </summary>
        public static readonly IReadOnlyCollection<Currency> All =
        [
            Usd,
            Cop,
            Eur
        ];
    }
}
