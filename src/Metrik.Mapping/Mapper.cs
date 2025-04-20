using Metrik.Mapping.Configuration;

namespace Metrik.Mapping
{
    /// <summary>
    /// Mapper class for mapping objects
    /// </summary>
    public sealed class Mapper : IMapper
    {
        /// <summary>
        /// The configuration provider for the mapper
        /// </summary>
        private readonly IConfigurationProvider _configurationProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="Mapper"/> class with the specified configuration provider
        /// </summary>
        /// <param name="configurationProvider">The configuration provider for the mapper</param>
        /// <exception cref="ArgumentNullException">If the configuration provider is null</exception>
        public Mapper(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider ?? throw new ArgumentNullException(nameof(configurationProvider));
        }

        /// <inheritdoc />
        public TDestination Map<TDestination>(object source)
        {
            return Map<object, TDestination>(source);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">If the mapping configuration is not found</exception>
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            if (source == null)
                return default;

            var typeMap = _configurationProvider.FindTypeMapFor(typeof(TSource), typeof(TDestination));
            if (typeMap == null)
                throw new InvalidOperationException($"Mapping configuration not found for {typeof(TSource).Name} -> {typeof(TDestination).Name}");

            return (TDestination)typeMap.Map(source);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">If the mapping configuration is not found</exception>
        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            if (source == null)
                return destination;

            var typeMap = _configurationProvider.FindTypeMapFor(typeof(TSource), typeof(TDestination));
            if (typeMap == null)
                throw new InvalidOperationException($"Mapping configuration not found for {typeof(TSource).Name} -> {typeof(TDestination).Name}");

            return (TDestination)typeMap.Map(source, destination);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">If the mapping configuration is not found</exception>
        public object Map(object source, object destination, Type sourceType, Type destinationType)
        {
            if (source == null)
                return destination;

            var typeMap = _configurationProvider.FindTypeMapFor(sourceType, destinationType);
            if (typeMap == null)
                throw new InvalidOperationException($"Mapping configuration not found for {sourceType.Name} -> {destinationType.Name}");

            return typeMap.Map(source, destination);
        }

        /// <inheritdoc />
        /// <exception cref="InvalidOperationException">If the mapping configuration is not found</exception>
        public object Map(object source, Type sourceType, Type destinationType)
        {
            if (source == null)
                return null;

            var typeMap = _configurationProvider.FindTypeMapFor(sourceType, destinationType);
            if (typeMap == null)
                throw new InvalidOperationException($"Mapping configuration not found for {sourceType.Name} -> {destinationType.Name}");

            return typeMap.Map(source);
        }

        /// <inheritdoc />
        public IConfigurationProvider ConfigurationProvider => _configurationProvider;
    }
}