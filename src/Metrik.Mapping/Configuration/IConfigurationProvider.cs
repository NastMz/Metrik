using Metrik.Mapping.Mapping;

namespace Metrik.Mapping.Configuration
{
    /// <summary>
    /// Interface for configuration provider
    /// </summary>
    public interface IConfigurationProvider
    {
        /// <summary>
        /// Gets the type maps for the configuration
        /// </summary>
        /// <param name="sourceType">The source type</param>
        /// <param name="destinationType">The destination type</param>
        /// <returns>The type map for the specified source and destination types</returns>
        ITypeMap FindTypeMapFor(Type sourceType, Type destinationType);

        /// <summary>
        /// Asserts that the configuration is valid
        /// </summary>
        void AssertConfigurationIsValid();
    }
}