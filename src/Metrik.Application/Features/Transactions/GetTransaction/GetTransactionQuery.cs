using Metrik.Application.Abstractions.Interfaces.Messaging;

namespace Metrik.Application.Features.Transactions.GetTransaction
{
    /// <summary>
    /// Query to retrieve a transaction by its unique identifier.
    /// </summary>
    /// <param name="TransactionId">The unique identifier of the transaction to retrieve.</param>
    public sealed record GetTransactionQuery(Guid TransactionId) : IQuery<TransactionResponse>;
}
