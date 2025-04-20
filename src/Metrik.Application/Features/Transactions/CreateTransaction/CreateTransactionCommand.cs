using Metrik.Application.Abstractions.Interfaces.Messaging;
using Metrik.Domain.Entities.Transactions.Enums;
using Metrik.Domain.Entities.Transactions.ValueObjects;

namespace Metrik.Application.Features.Transactions.CreateTransaction
{
    /// <summary>
    /// Command to create a new transaction.
    /// </summary>
    /// <param name="UserId">The identifier of the user creating the transaction.</param>
    /// <param name="AccountId">The identifier of the account associated with the transaction.</param>
    /// <param name="CategoryId">The identifier of the category associated with the transaction.</param>
    /// <param name="Amount">The amount of the transaction.</param>
    /// <param name="Type">The type of the transaction (e.g., income, expense).</param>
    /// <param name="Description">The description of the transaction.</param>
    public record CreateTransactionCommand(
        Guid UserId,
        Guid AccountId,
        Guid CategoryId,
        decimal Amount,
        TransactionType Type,
        TransactionDescription Description
    ) : ICommand<Guid>;
}
