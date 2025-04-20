using Metrik.Domain.Abstractions.Models;

namespace Metrik.Domain.Entities.Accounts.Errors
{
    /// <summary>
    /// Static class containing error definitions related to accounts.
    /// </summary>
    public static class AccountErrors
    {
        /// <summary>
        /// Error indicating that an account was not found.
        /// </summary>
        public static readonly Error NotFound = new(
            "Account.NotFound",
            "The account with the specified identifier was not found.",
            ErrorType.NotFound,
            "Errors.Account.NotFound"
        );
    }
}
