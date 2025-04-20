using Metrik.Domain.Abstractions.Models;

namespace Metrik.Domain.Abstractions.Interfaces
{
    /// <summary>
    /// Defines basic operations for accessing and persisting entities.
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity</typeparam>
    public interface IRepository<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Retrieves an entity by its identifier.
        /// </summary>
        /// <param name="id">Identifier of the entity.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>The entity if it exists, null otherwise.</returns>
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">Entity to add.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        void Add(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        void Update(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">Entity to delete.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        void Delete(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if an entity with the specified identifier exists.
        /// </summary>
        /// <param name="id">Identifier of the entity.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>True if it exists, false otherwise.</returns>
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
