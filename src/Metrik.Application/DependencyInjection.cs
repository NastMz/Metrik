using FluentValidation;
using Metrik.Domain.Entities.Transactions.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Metrik.Application
{
    /// <summary>
    /// Dependency injection configuration for the application layer.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers application services in the provided service collection.
        /// </summary>
        /// <param name="services">The service collection to register services in.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<TransactionService>();

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            return services;
        }
    }
}
