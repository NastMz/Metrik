using Metrik.Domain.Abstractions.Models;

namespace Metrik.Domain.Entities.Transactions.Errors
{
    /// <summary>
    /// Static class containing error definitions related to transactions.
    /// </summary>
    public static class TransactionErrors
    {
        /// <summary>
        /// Error indicating that a transaction was not found.
        /// </summary>
        public static readonly Error NotFound = new(
            "Transaction.NotFound",
            "The transaction with the specified identifier was not found.",
            ErrorType.NotFound,
            "Errors.Transaction.NotFound"
        );

        /// <summary>
        /// Error indicating that a transaction is not valid.
        /// </summary>
        public static readonly Error NotEnoughBalance = new(
            "Transaction.Balance",
            "The transaction cannot be completed due to insufficient balance.",
            ErrorType.Validation,
            "Errors.Transaction.NotEnoughBalance"
        );

        /// <summary>
        /// Error indicating that the transaction amount is invalid.
        /// </summary>
        public static readonly Error InvalidAmount = new(
            "Transaction.Amount",
            "The transaction amount is invalid.",
            ErrorType.Validation,
            "Errors.Transaction.InvalidAmount"
        );

        /// <summary>
        /// Error indicating that the transaction is unauthorized.
        /// </summary>
        public static readonly Error Unauthorized = new(
            "Transaction.Unauthorized",
            "You are not authorized to access this transaction.",
            ErrorType.Unauthorized,
            "Errors.Transaction.Unauthorized"
        );
    }
}