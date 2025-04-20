using Metrik.Mapping.Configuration;

namespace Metrik.Mapping
{
    /// <summary>
    /// Mapper interface for mapping objects
    /// </summary>
    public interface IMapper
    {
        /// <summary>
        /// Maps the source object to the destination type
        /// </summary>
        /// <typeparam name="TDestination">The type of the destination object</typeparam>
        /// <param name="source">The source object to map from</param>
        /// <returns>The mapped destination object</returns>
        TDestination Map<TDestination>(object source);

        /// <summary>
        /// Maps the source object to the destination type
        /// </summary>
        /// <typeparam name="TSource">The type of the source object</typeparam>
        /// <typeparam name="TDestination">The type of the destination object</typeparam>
        /// <param name="source">The source object to map from</param>
        /// <returns>The mapped destination object</returns>
        TDestination Map<TSource, TDestination>(TSource source);

        /// <summary>
        /// Maps the source object to the destination object
        /// </summary>
        /// <typeparam name="TSource">The type of the source object</typeparam>
        /// <typeparam name="TDestination">The type of the destination object</typeparam>
        /// <param name="source">The source object to map from</param>
        /// <param name="destination">The destination object to map to</param>
        /// <returns>The mapped destination object</returns>
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);

        /// <summary>
        /// Maps the source object to the destination object
        /// </summary>
        /// <param name="source">The source object to map from</param>
        /// <param name="destination">The destination object to map to</param>
        /// <param name="sourceType">The type of the source object</param>
        /// <param name="destinationType">The type of the destination object</param>
        /// <returns></returns>
        object Map(object source, object destination, Type sourceType, Type destinationType);

        /// <summary>
        /// Maps the source object to the destination type
        /// </summary>
        /// <param name="source">The source object to map from</param>
        /// <param name="sourceType">The type of the source object</param>
        /// <param name="destinationType">The type of the destination object</param>
        /// <returns>The mapped destination object</returns>
        object Map(object source, Type sourceType, Type destinationType);

        /// <summary>
        /// Gets the configuration provider for the mapper
        /// </summary>
        IConfigurationProvider ConfigurationProvider { get; }
    }
}