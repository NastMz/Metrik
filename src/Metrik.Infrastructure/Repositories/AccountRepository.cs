using Metrik.Domain.Entities.Accounts;
using Metrik.Domain.Entities.Accounts.Repository;

namespace Metrik.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for account entities.
    /// </summary>
    internal sealed class AccountRepository : Repository<Account>, IAccountRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context used for data access.</param>
        public AccountRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
