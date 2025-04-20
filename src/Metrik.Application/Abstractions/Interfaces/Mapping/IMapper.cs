namespace Metrik.Application.Abstractions.Interfaces.Mapping
{
    /// <summary>
    /// Main interface for performing mapping operations.
    /// </summary>
    public interface IMapper
    {
        /// <summary>
        /// Maps a source object to a new destination object.
        /// </summary>
        TDestination Map<TSource, TDestination>(TSource source) where TDestination : new();

        /// <summary>
        /// Maps a source object to an existing destination object.
        /// </summary>
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);

        /// <summary>
        /// Maps a source object to a specific destination type.
        /// </summary>
        object Map(object source, Type sourceType, Type destinationType);

        /// <summary>
        /// Maps a source object to an existing destination object.
        /// </summary>
        object Map(object source, object destination, Type sourceType, Type destinationType);

        /// <summary>
        /// Projects an IQueryable based on the mapping configuration.
        /// </summary>
        IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source);
    }
}
