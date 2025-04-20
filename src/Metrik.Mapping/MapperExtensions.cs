namespace Metrik.Mapping
{
    /// <summary>
    /// Extensions for the IMapper interface
    /// </summary>
    public static class MapperExtensions
    {
        /// <summary>
        /// Maps the source object to the destination type
        /// </summary>
        /// <typeparam name="TDestination">The type of the destination object</typeparam>
        /// <param name="mapper">The mapper instance</param>
        /// <param name="source">The source object to map from</param>
        /// <returns>The mapped destination object</returns>
        public static TDestination Map<TDestination>(this IMapper mapper, object source)
        {
            return mapper.Map<TDestination>(source);
        }

        /// <summary>
        /// Maps the source object to the destination type
        /// </summary>
        /// <typeparam name="TSource">The type of the source object</typeparam>
        /// <typeparam name="TDestination">The type of the destination object</typeparam>
        /// <param name="mapper">The mapper instance</param>
        /// <param name="source">The source object to map from</param>
        /// <returns>The mapped destination object</returns>
        public static TDestination Map<TSource, TDestination>(this IMapper mapper, TSource source)
        {
            return mapper.Map<TSource, TDestination>(source);
        }

        /// <summary>
        /// Maps the source object to the destination object
        /// </summary>
        /// <typeparam name="TSource">The type of the source object</typeparam>
        /// <typeparam name="TDestination">The type of the destination object</typeparam>
        /// <param name="mapper">The mapper instance</param>
        /// <param name="source">The source object to map from</param>
        /// <param name="destination">The destination object to map to</param>
        /// <returns>The mapped destination object</returns>
        public static TDestination Map<TSource, TDestination>(this IMapper mapper, TSource source, TDestination destination)
        {
            return mapper.Map(source, destination);
        }
    }
}