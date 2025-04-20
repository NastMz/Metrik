using Metrik.Domain.Entities.Transactions;
using Metrik.Domain.Entities.Transactions.Repository;

namespace Metrik.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for transaction entities.
    /// </summary>
    internal sealed class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context used for data access.</param>
        public TransactionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
