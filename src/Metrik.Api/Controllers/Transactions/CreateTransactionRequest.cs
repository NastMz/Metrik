namespace Metrik.Api.Controllers.Transactions
{
    /// <summary>
    /// Request model for creating a new transaction.
    /// </summary>
    /// <param name="UserId">The identifier of the user creating the transaction.</param>
    /// <param name="AccountId">The identifier of the account associated with the transaction.</param>
    /// <param name="CategoryId">The identifier of the category associated with the transaction.</param>
    /// <param name="Amount">The amount of the transaction.</param>
    /// <param name="Type">The type of the transaction (e.g., income, expense).</param>
    /// <param name="Description">The description of the transaction.</param>
    public sealed record CreateTransactionRequest(
        Guid UserId,
        Guid AccountId,
        Guid CategoryId,
        decimal Amount,
        int Type,
        string Description);
}
