using Metrik.Mapping.Configuration;

namespace Metrik.Mapping
{
    /// <summary>
    /// Static class for initializing and accessing the AutoMapper instance
    /// </summary>
    public static class AutoMapper
    {
        /// <summary>
        /// Static instance of the IMapper
        /// </summary>
        private static IMapper _mapper;

        /// <summary>
        /// Initializes the AutoMapper with the provided configuration
        /// </summary>
        /// <param name="config">The configuration action to set up the mapper</param>
        public static void Initialize(Action<IMapperConfigurationExpression> config)
        {
            var mapperConfig = new MapperConfiguration(config);
            _mapper = mapperConfig.CreateMapper();
        }

        /// <summary>
        /// Gets the static instance of the IMapper
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the mapper is not initialized</exception>
        public static IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                    throw new InvalidOperationException("Mapper not initialized. Call Initialize with appropriate configuration.");

                return _mapper;
            }
        }
    }
}