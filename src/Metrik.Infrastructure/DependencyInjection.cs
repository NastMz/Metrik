using Dapper;
using Metrik.Application.Abstractions.Interfaces.Clock;
using Metrik.Application.Abstractions.Interfaces.Data;
using Metrik.Application.Abstractions.Interfaces.Email;
using Metrik.Domain.Abstractions.Interfaces;
using Metrik.Infrastructure.Clock;
using Metrik.Infrastructure.Data;
using Metrik.Infrastructure.Email;
using Metrik.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Metrik.Infrastructure
{
    /// <summary>
    /// Dependency injection configuration for the infrastructure layer.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds the infrastructure services to the service collection.
        /// </summary>
        /// <param name="services">The service collection to add services to.</param>
        /// <param name="configuration">The configuration to use for setting up services.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();

            services.AddTransient<IEmailService, EmailService>();

            var connectionString = configuration.GetConnectionString("Database")
                ?? throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
            });

            services.AddRepositories();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

            return services;
        }

        /// <summary>
        /// Adds repository services to the service collection.
        /// </summary>
        /// <param name="services">The service collection to add services to.</param>
        /// <returns>The updated service collection.</returns>
        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            var repositoryTypes = typeof(Repository<>).Assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract &&
                       t.GetInterfaces().Any(i => i.IsGenericType &&
                       i.GetGenericTypeDefinition() == typeof(IRepository<>)));

            foreach (var type in repositoryTypes)
            {
                var interfaces = type.GetInterfaces()
                    .Where(i => i != typeof(IDisposable) &&
                           i.Name != typeof(IRepository<>).Name);

                foreach (var iface in interfaces)
                {
                    services.AddScoped(iface, type);
                }
            }

            return services;
        }
    }
}
