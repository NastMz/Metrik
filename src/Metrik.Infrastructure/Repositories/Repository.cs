using Metrik.Domain.Abstractions.Interfaces;
using Metrik.Domain.Abstractions.Models;
using Microsoft.EntityFrameworkCore;

namespace Metrik.Infrastructure.Repositories
{
    /// <summary>
    /// Base class for repositories that provides basic CRUD operations.
    /// </summary>
    /// <typeparam name="T">Type of the entity</typeparam>
    internal abstract class Repository<T> : IRepository<T> where T : Entity
    {
        /// <summary>
        /// The database context used for data access.
        /// </summary>
        protected readonly ApplicationDbContext DbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class with the specified database context.
        /// </summary>
        /// <param name="dbContext"></param>
        protected Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        /// <inheritdoc />
        public void Add(T entity, CancellationToken cancellationToken = default)
        {
            DbContext.Add(entity);
        }

        /// <inheritdoc />
        public void Delete(T entity, CancellationToken cancellationToken = default)
        {
            DbContext.Remove(entity);
        }

        /// <inheritdoc />
        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<T>().AnyAsync(e => e.Id == id, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<T>().FindAsync([id], cancellationToken);
        }

        /// <inheritdoc />
        public void Update(T entity, CancellationToken cancellationToken = default)
        {
            DbContext.Update(entity);
        }
    }
}
