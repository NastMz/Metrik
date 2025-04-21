using Metrik.Mapping.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Metrik.Mapping
{
    /// <summary>
    /// Extensions for registering Mapper with dependency injection containers
    /// </summary>
    public static class ServiceRegistrationExtensions
    {
        /// <summary>
        /// Adds Mapper to the specified IServiceCollection
        /// </summary>
        /// <param name="services">The IServiceCollection to add services to</param>
        /// <param name="configAction">Action to configure the mapper</param>
        /// <returns>The IServiceCollection for further configuration</returns>
        public static IServiceCollection AddMapper(this IServiceCollection services, Action<IMapperConfigurationExpression> configAction)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configAction == null)
                throw new ArgumentNullException(nameof(configAction));

            var config = new MapperConfiguration(configAction);

            // Validate configuration
            config.AssertConfigurationIsValid();

            // Register the configuration
            services.AddSingleton<IConfigurationProvider>(config);

            // Register the mapper
            services.AddSingleton<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>()));

            return services;
        }

        /// <summary>
        /// Adds Mapper to the specified IServiceCollection and scans the specified assemblies for profiles
        /// </summary>
        /// <param name="services">The IServiceCollection to add services to</param>
        /// <param name="assemblies">The assemblies to scan for profiles</param>
        /// <returns>The IServiceCollection for further configuration</returns>
        public static IServiceCollection AddMapper(this IServiceCollection services, params Assembly[] assemblies)
        {
            return AddMapper(services, config =>
            {
                bool profilesFound = false;

                // Find all Profile classes in the specified assemblies
                foreach (var assembly in assemblies)
                {
                    var profileTypes = assembly.GetTypes()
                        .Where(t => typeof(Profile).IsAssignableFrom(t) &&
                                   !t.IsAbstract &&
                                   t.GetConstructor(Type.EmptyTypes) != null);

                    foreach (var profileType in profileTypes)
                    {
                        // Add profile to configuration
                        config.AddProfile(Activator.CreateInstance(profileType) as Profile);
                        profilesFound = true;
                    }
                }

                // Add a default empty mapping if no profiles found
                if (!profilesFound)
                {
                    // Add a dummy/empty mapping to prevent validation error
                    config.CreateMap<object, object>();
                }
            });
        }

        /// <summary>
        /// Adds Mapper to the specified IServiceCollection and scans the calling assembly for profiles
        /// </summary>
        /// <param name="services">The IServiceCollection to add services to</param>
        /// <returns>The IServiceCollection for further configuration</returns>
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            return AddMapper(services, Assembly.GetCallingAssembly());
        }

        /// <summary>
        /// Adds Mapper to the specified IServiceCollection and scans the specified assemblies for profiles
        /// </summary>
        /// <param name="services">The IServiceCollection to add services to</param>
        /// <param name="assemblyMarkerTypes">Types to use as markers for assemblies to scan</param>
        /// <returns>The IServiceCollection for further configuration</returns>
        public static IServiceCollection AddMapper(this IServiceCollection services, params Type[] assemblyMarkerTypes)
        {
            var assemblies = assemblyMarkerTypes.Select(t => t.Assembly).ToArray();
            return AddMapper(services, assemblies);
        }

        /// <summary>
        /// Adds Mapper to the specified IServiceCollection with the provided mapper configuration
        /// </summary>
        /// <param name="services">The IServiceCollection to add services to</param>
        /// <param name="config">The mapper configuration</param>
        /// <returns>The IServiceCollection for further configuration</returns>
        public static IServiceCollection AddMapper(this IServiceCollection services, MapperConfiguration config)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (config == null)
                throw new ArgumentNullException(nameof(config));

            // Register the configuration
            services.AddSingleton<IConfigurationProvider>(config);

            // Register the mapper
            services.AddSingleton<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>()));

            return services;
        }
    }
}
