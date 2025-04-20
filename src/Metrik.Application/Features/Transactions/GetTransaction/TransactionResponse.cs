namespace Metrik.Application.Features.Transactions.GetTransaction
{
    /// <summary>
    /// Represents the response for a transaction query.
    /// </summary>
    public sealed class TransactionResponse
    {
        /// <summary>
        /// The unique identifier for the transaction.
        /// </summary>
        public Guid Id { get; init; }

        // <summary>
        /// The unique identifier for the user associated with the transaction.
        /// </summary>
        public Guid UserId { get; init; }

        /// <summary>
        /// The unique identifier for the account associated with the transaction.
        /// </summary>
        public Guid AccountId { get; init; }

        /// <summary>
        /// The unique identifier for the category associated with the transaction.
        /// </summary>
        public Guid CategoryId { get; init; }

        /// <summary>
        /// The amount of money involved in the transaction.
        /// </summary>
        public decimal Amount { get; init; }

        /// <summary>
        /// The type of the transaction (e.g., income or expense).
        /// </summary>
        public string Type { get; init; }

        /// <summary>
        /// The description of the transaction.
        /// </summary>
        public string Description { get; init; }

        /// <summary>
        /// The date of the transaction.
        /// </summary>
        public DateTime Date { get; init; }
    }
}
