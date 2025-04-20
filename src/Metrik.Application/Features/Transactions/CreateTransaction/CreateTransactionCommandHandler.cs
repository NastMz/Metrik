using Metrik.Application.Abstractions.Interfaces.Clock;
using Metrik.Application.Abstractions.Interfaces.Messaging;
using Metrik.Application.Exceptions;
using Metrik.Domain.Abstractions.Interfaces;
using Metrik.Domain.Abstractions.Models;
using Metrik.Domain.Entities.Accounts.Errors;
using Metrik.Domain.Entities.Accounts.Repository;
using Metrik.Domain.Entities.Categories.Errors;
using Metrik.Domain.Entities.Categories.Repository;
using Metrik.Domain.Entities.Transactions;
using Metrik.Domain.Entities.Transactions.Errors;
using Metrik.Domain.Entities.Transactions.Repository;
using Metrik.Domain.Entities.Transactions.Services;
using Metrik.Domain.Entities.Users.Errors;
using Metrik.Domain.Entities.Users.Repository;
using Metrik.Domain.Shared.ValueObjects;

namespace Metrik.Application.Features.Transactions.CreateTransaction
{
    /// <summary>
    /// Handles the creation of a new transaction.
    /// </summary>
    internal sealed class CreateTransactionCommandHandler : ICommandHandler<CreateTransactionCommand, Guid>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly TransactionService _transactionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTransactionCommandHandler"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="accountRepository">The account repository.</param>
        /// <param name="categoryRepository">The category repository.</param>
        /// <param name="transactionRepository">The transaction repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="dateTimeProvider">The date time provider.</param>
        /// <param name="transactionService">The transaction service.</param>
        public CreateTransactionCommandHandler(
            IUserRepository userRepository,
            IAccountRepository accountRepository,
            ICategoryRepository categoryRepository,
            ITransactionRepository transactionRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            TransactionService transactionService
        )
        {
            _userRepository = userRepository;
            _accountRepository = accountRepository;
            _categoryRepository = categoryRepository;
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _transactionService = transactionService;
        }

        /// <summary>
        /// Handles the creation of a new transaction.
        /// </summary>
        /// <param name="request">The command containing the transaction details.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the transaction creation operation, including the transaction ID if successful.</returns>
        public async Task<Result<Guid>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

            if (user is null)
            {
                return Result.Failure<Guid>(UserErrors.NotFound);
            }

            var account = await _accountRepository.GetByIdAsync(request.AccountId, cancellationToken);

            if (account is null)
            {
                return Result.Failure<Guid>(AccountErrors.NotFound);
            }

            var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);

            if (category is null)
            {
                return Result.Failure<Guid>(CategoryErrors.NotFound);
            }

            try
            {
                var transaction = Transaction.Create(
                    account,
                    request.CategoryId,
                    new Money(request.Amount, account.Balance.Currency),
                    request.Type,
                    request.Description,
                    _dateTimeProvider.UtcNow,
                    _transactionService
                );

                if (transaction.IsFailure)
                {
                    return Result.Failure<Guid>(transaction.Error);
                }

                _transactionRepository.Add(transaction.Value, cancellationToken);

                _accountRepository.Update(account, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return transaction.Value.Id;
            }
            catch (ConcurrencyException)
            {
                return Result.Failure<Guid>(TransactionErrors.Concurrency);
            }
        }
    }
}