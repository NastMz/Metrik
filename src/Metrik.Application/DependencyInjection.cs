using FluentValidation;
using Metrik.Application.Abstractions.Behaviors;
using Metrik.Domain.Entities.Transactions.Services;
using Metrik.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Nast.SimpleMediator;
using Nast.SimpleMediator.Abstractions;

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

            services.AddMediator(options =>
            {
                options.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);

                options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
                options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            services.AddMapper();

            return services;
        }
    }
}
