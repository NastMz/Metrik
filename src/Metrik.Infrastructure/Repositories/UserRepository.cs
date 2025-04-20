using Metrik.Domain.Entities.Users;
using Metrik.Domain.Entities.Users.Repository;

namespace Metrik.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for user entities.
    /// </summary>
    internal sealed class UserRepository : Repository<User>, IUserRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context used for data access.</param>
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
