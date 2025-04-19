using Metrik.Domain.Entities.Accounts;
using Metrik.Domain.Entities.Transactions.Enums;
using Metrik.Domain.Shared.ValueObjects;

namespace Metrik.Domain.Entities.Transactions.Services
{
    public class TransactionService
    {
        /// <summary>
        /// Calculates the balance of an account after a transaction.
        /// </summary>
        /// <param name="account">The account to calculate the balance for.</param>
        /// <param name="transaction">The transaction to apply.</param>
        /// <returns>The new balance of the account after the transaction.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the currency of the account and transaction do not match.</exception>
        public Money CalculateBalanceAfterTransaction(Account account, Transaction transaction)
        {
            if (account.Balance.Currency != transaction.Amount.Currency)
            {
                throw new InvalidOperationException("Currency mismatch between account and transaction.");
            }

            if (transaction.Type == TransactionType.Income)
            {
                return account.Balance + transaction.Amount;
            }
            else if (transaction.Type == TransactionType.Expense || transaction.Type == TransactionType.Transfer)
            {
                return account.Balance - transaction.Amount;
            }

            return account.Balance;
        }
    }
}
