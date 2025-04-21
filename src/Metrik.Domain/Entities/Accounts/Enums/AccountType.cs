namespace Metrik.Domain.Entities.Accounts.Enums
{
    /// <summary>
    /// Represents the type of an account.
    /// </summary>
    /// <param name="Value">The type of the account.</param>
    public enum AccountType
    {
        /// <summary>
        /// Represents a bank account.
        /// </summary>
        Bank = 1,

        /// <summary>
        /// Represents a cash account.
        /// </summary>
        Cash = 2,

        /// <summary>
        /// Represents a credit card account.
        /// </summary>
        CreditCard = 3,

        /// <summary>
        /// Represents an investment account.
        /// </summary>
        Investment = 4,
    }
}
