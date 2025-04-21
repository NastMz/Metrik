using Metrik.Domain.Abstractions.Models;
using Metrik.Domain.Entities.Accounts.Enums;
using Metrik.Domain.Entities.Accounts.ValueObjects;
using Metrik.Domain.Shared.ValueObjects;

namespace Metrik.Domain.Entities.Accounts
{
    /// <summary>
    /// Represents a bank account with a name, balance, and currency.
    /// </summary>
    public sealed class Account : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Account"/> class with the specified parameters.
        /// </summary>
        /// <param name="id">The unique identifier for the account.</param>
        /// <param name="userId">The unique identifier for the user associated with the account.</param>
        /// <param name="name">The name of the account.</param>
        /// <param name="balance">The balance of the account.</param>
        public Account(Guid id, Guid userId, AccountName name, AccountType type, Money balance) : base(id)
        {
            UserId = userId;
            Name = name;
            Type = type;
            Balance = balance;
        }

        /// <summary>
        /// Default constructor for the Account class.
        /// </summary>
        private Account()
        {
        }

        /// <summary>
        /// The unique identifier for the user associated with the account.
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// AccountName of the account.
        /// </summary>
        public AccountName Name { get; private set; }

        /// <summary>
        /// Type of the account.
        /// </summary>
        public AccountType Type { get; private set; }

        /// <summary>
        /// Balance of the account.
        /// </summary>
        public Money Balance { get; internal set; }
    }
}
