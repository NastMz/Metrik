using Dapper;
using Metrik.Application.Abstractions.Interfaces.Data;
using Metrik.Application.Abstractions.Interfaces.Messaging;
using Metrik.Domain.Abstractions.Models;
using Metrik.Domain.Entities.Transactions.Errors;

namespace Metrik.Application.Features.Transactions.GetTransaction
{
    internal sealed class GetTransactionQueryHandler : IQueryHandler<GetTransactionQuery, TransactionResponse>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTransactionQueryHandler"/> class.
        /// </summary>
        /// <param name="sqlConnectionFactory">The SQL connection factory used to create database connections.</param>
        public GetTransactionQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        /// <summary>
        /// Handles the query to retrieve a transaction by its unique identifier.
        /// </summary>
        /// <param name="request">The query request containing the unique identifier of the transaction to retrieve.</param>
        /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
        /// <returns>The result of the query containing the transaction response if successful, or an error if the transaction could not be retrieved.</returns>
        public async Task<Result<TransactionResponse>> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT 
                    id as Id,
                    account_id as AccountId,
                    user_id as UserId,
                    category_id as CategoryId,
                    value_amount as Amount,
                    value_currency as Currency,
                    type as Type,
                    description as Description,
                    date as Date
                FROM transactions
                WHERE id = @TransactionId";

            var transaction = await connection.QueryFirstOrDefaultAsync<TransactionResponse>(
                sql,
                new
                {
                    request.TransactionId
                }
            );

            return transaction is not null
                ? Result.Success(transaction)
                : Result.Failure<TransactionResponse>(TransactionErrors.NotFound);
        }
    }
}
