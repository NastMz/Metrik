namespace Metrik.Mapping.Mapping
{
    /// <summary>
    /// Interface for type mapping configuration.
    /// </summary>
    public interface ITypeMap
    {
        /// <summary>
        /// Gets the source type for the mapping.
        /// </summary>
        Type SourceType { get; }

        /// <summary>
        /// Gets the destination type for the mapping.
        /// </summary>
        Type DestinationType { get; }

        /// <summary>
        /// Maps the source object to the destination type.
        /// </summary>
        /// <param name="source">The source object to map from.</param>
        /// <returns>The mapped destination object.</returns>
        object Map(object source);

        /// <summary>
        /// Maps the source object to the destination object.
        /// </summary>
        /// <param name="source">The source object to map from.</param>
        /// <param name="destination">The destination object to map to.</param>
        /// <returns>The mapped destination object.</returns>
        object Map(object source, object destination);
    }
}