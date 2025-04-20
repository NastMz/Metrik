using Metrik.Mapping.Configuration;
using Metrik.Mapping.Mapping;

namespace Metrik.Mapping
{
    /// <summary>
    /// Base class for defining mapping profiles.
    /// </summary>
    public class Profile : IProfileConfiguration
    {
        /// <inheritdoc />
        public string ProfileName { get; }

        /// <inheritdoc />
        public List<ITypeMap> TypeMaps { get; } = [];

        /// <summary>
        /// Reference to the configuration expression used to create maps.
        /// </summary>
        protected IMapperConfigurationExpression ConfigurationExpression { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Profile"/> class.
        /// </summary>
        public Profile()
        {
            ProfileName = GetType().Name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Profile"/> class with a specified profile name.
        /// </summary>
        /// <param name="profileName">The name of the profile.</param>
        public Profile(string profileName)
        {
            ProfileName = profileName;
        }

        /// <summary>
        /// Configures the profile with the provided configuration expression.
        /// </summary>
        /// <param name="config">The configuration expression to use for mapping.</param>
        public void Configure(IMapperConfigurationExpression config)
        {
            ConfigurationExpression = config;
            Configure();
        }

        /// <inheritdoc />
        public virtual void Configure()
        {
            // Override in derived classes to add mappings
        }

        /// <summary>
        /// Creates a map between the specified source and destination types.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TDestination">The destination type.</typeparam>
        /// <returns>The mapping expression.</returns>
        protected IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>()
        {
            var map = ConfigurationExpression.CreateMap<TSource, TDestination>();
            return map;
        }
    }

    /// <summary>
    /// Interface for defining mapping profiles.
    /// </summary>
    public interface IProfileConfiguration
    {
        /// <summary>
        /// Gets the name of the profile.
        /// </summary>
        string ProfileName { get; }

        /// <summary>
        /// Gets the list of type maps defined in this profile.
        /// </summary>
        List<ITypeMap> TypeMaps { get; }

        /// <summary>
        /// Configures the profile with the provided configuration expression.
        /// </summary>
        void Configure();
    }
}
