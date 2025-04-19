using Metrik.Domain.Abstractions.Models;

namespace Metrik.Domain.Entities.Transactions.Errors
{
    public static class TransactionErrors
    {
        public static readonly Error NotFound = new(
            "Transaction.Found",
            "The transaction with the specified identifier was not found."
        );

        public static readonly Error NotEnoughBalance = new(
            "Transaction.Balance",
            "The transaction cannot be completed due to insufficient balance."
        );

        public static readonly Error InvalidAmount = new(
            "Transaction.Amount",
            "The transaction amount is invalid."
        );
    }
}
