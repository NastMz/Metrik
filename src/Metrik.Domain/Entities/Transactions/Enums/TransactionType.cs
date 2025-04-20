namespace Metrik.Domain.Entities.Transactions.Enums
{
    /// <summary>
    /// Represents the type of a transaction, such as "income" or "expense".
    /// </summary>
    public enum TransactionType
    {
        /// <summary>
        /// Indicates that the transaction is an income.
        /// </summary>
        Income = 1,

        /// <summary>
        /// Indicates that the transaction is an expense.
        /// </summary>
        Expense = 2,

        /// <summary>
        /// Indicates that the transaction is a transfer between accounts.
        /// </summary>
        Transfer = 3
    }
}
