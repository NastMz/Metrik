using Metrik.Domain.Abstractions.Models;
using Metrik.Domain.Entities.Accounts;
using Metrik.Domain.Entities.Accounts.Events;
using Metrik.Domain.Entities.Transactions.Enums;
using Metrik.Domain.Entities.Transactions.Errors;
using Metrik.Domain.Entities.Transactions.Events;
using Metrik.Domain.Entities.Transactions.Services;
using Metrik.Domain.Entities.Transactions.ValueObjects;
using Metrik.Domain.Shared.ValueObjects;

namespace Metrik.Domain.Entities.Transactions
{
    /// <summary>
    /// Represents a financial transaction in the system.
    /// </summary>
    public class Transaction : Entity
    {
        /// <summary>
        /// Creates a new transaction instance with the specified parameters.
        /// </summary>
        /// <param name="id">The unique identifier for the transaction.</param>
        /// <param name="userId">The unique identifier for the user associated with the transaction.</param>
        /// <param name="accountId">The unique identifier for the account associated with the transaction.</param>
        /// <param name="categoryId">The unique identifier for the category associated with the transaction.</param>
        /// <param name="amount">The amount of money involved in the transaction.</param>
        /// <param name="type">The type of the transaction (e.g., income or expense).</param>
        /// <param name="description">The description of the transaction.</param>
        /// <param name="date">The date of the transaction.</param>
        private Transaction(Guid id, Guid userId, Guid accountId, Guid categoryId, Money amount, TransactionType type, TransactionDescription description, DateTime date)
            : base(id)
        {
            UserId = userId;
            AccountId = accountId;
            CategoryId = categoryId;
            Amount = amount;
            Type = type;
            Description = description;
            Date = date;
        }

        /// <summary>
        /// The unique identifier for the user associated with the transaction.
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// The unique identifier for the account associated with the transaction.
        /// </summary>
        public Guid AccountId { get; private set; }

        /// <summary>
        /// The unique identifier for the category associated with the transaction.
        /// </summary>
        public Guid CategoryId { get; private set; }

        /// <summary>
        /// The amount of money involved in the transaction.
        /// </summary>
        public Money Amount { get; private set; }

        /// <summary>
        /// The type of the transaction (e.g., income or expense).
        /// </summary>
        public TransactionType Type { get; private set; }

        /// <summary>
        /// The description of the transaction.
        /// </summary>
        public TransactionDescription Description { get; private set; }

        /// <summary>
        /// The date of the transaction.
        /// </summary>
        public DateTime Date { get; private set; }

        public static Result<Transaction> Create(
            Account account,
            Guid categoryId,
            Money amount,
            TransactionType type,
            TransactionDescription description,
            DateTime date,
            TransactionService transactionService
        )
        {
            if (amount.Amount <= 0)
            {
                return Result.Failure<Transaction>(TransactionErrors.InvalidAmount);
            }

            var transaction = new Transaction(Guid.NewGuid(), account.UserId, account.Id, categoryId, amount, type, description, date);

            var newBalance = transactionService.CalculateBalanceAfterTransaction(account, transaction);

            if (newBalance.Amount < 0)
            {
                return Result.Failure<Transaction>(TransactionErrors.NotEnoughBalance);
            }

            // Update the account balance
            account.Balance = newBalance;
            account.UpdatedAt = date;

            // Raise a domain event for transaction creation
            transaction.RaiseDomainEvent(new TransactionCreatedDomainEvent(transaction.Id));
            transaction.RaiseDomainEvent(new AccountUpdatedDomainEvent(account.Id, account.Balance));

            return Result.Success(transaction);
        }
    }
}