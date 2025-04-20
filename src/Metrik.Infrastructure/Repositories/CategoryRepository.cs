using Metrik.Domain.Entities.Categories;
using Metrik.Domain.Entities.Categories.Repository;

namespace Metrik.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for category entities.
    /// </summary>
    internal sealed class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context used for data access.</param>
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
