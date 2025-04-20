using Metrik.Application.Abstractions.Interfaces.Messaging;
using Metrik.Application.Exceptions;
using Metrik.Domain.Abstractions.Interfaces;
using Metrik.Domain.Abstractions.Models;
using Microsoft.EntityFrameworkCore;

namespace Metrik.Infrastructure
{
    /// <summary>
    /// Represents the application database context.
    /// </summary>
    public sealed class ApplicationDbContext : DbContext, IUnitOfWork
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class with the specified options.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        /// <inheritdoc />
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await base.SaveChangesAsync(cancellationToken);

                await PublishDomainEventsAsync(cancellationToken);

                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyException("Concurrency conflict occurred while saving changes.", ex);
            }
        }

        /// <summary>
        /// Publishes domain events for all entities that have them.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <returns>The task representing the asynchronous operation.</returns>
        private async Task PublishDomainEventsAsync(CancellationToken cancellationToken)
        {
            var domainEvents = ChangeTracker
                .Entries<Entity>()
                .Select(e => e.Entity)
                .SelectMany(e =>
                {
                    var domainEvents = e.GetDomainEvents();

                    e.ClearDomainEvents();

                    return domainEvents;
                })
                .ToList();

            await _domainEventDispatcher.DispatchEventsAsync(domainEvents, cancellationToken);
        }
    }
}
