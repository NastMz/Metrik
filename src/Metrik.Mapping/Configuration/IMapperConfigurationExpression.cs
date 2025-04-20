using Metrik.Mapping.Mapping;

namespace Metrik.Mapping.Configuration
{
    /// <summary>
    /// Interface for configuring the Mapper
    /// </summary>
    public interface IMapperConfigurationExpression
    {
        /// <summary>
        /// Creates a map between the specified source and destination types
        /// </summary>
        /// <typeparam name="TSource">The source type</typeparam>
        /// <typeparam name="TDestination">The destination type</typeparam>
        /// <returns>The mapping expression</returns>
        IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>();

        /// <summary>
        /// Creates a map between the specified source and destination types
        /// </summary>
        /// <param name="sourceType">The source type</param>
        /// <param name="destinationType">The destination type</param>
        /// <returns>The mapping expression</returns>
        IMappingExpression CreateMap(Type sourceType, Type destinationType);

        /// <summary>
        /// Adds a profile to the configuration
        /// </summary>
        /// <typeparam name="TProfile">The profile type</typeparam>
        void AddProfile<TProfile>() where TProfile : Profile, new();

        /// <summary>
        /// Adds a profile to the configuration
        /// </summary>
        /// <param name="profile">The profile instance</param>
        void AddProfile(Profile profile);
    }
}