using Metrik.Mapping.Mapping;

namespace Metrik.Mapping.Configuration
{
    /// <summary>
    /// Represents the configuration for the Mapper.
    /// </summary>
    public sealed class MapperConfiguration : IConfigurationProvider, IMapperConfigurationExpression
    {
        /// <summary>
        /// The dictionary that stores the type maps.
        /// </summary>
        private readonly Dictionary<TypePair, ITypeMap> _typeMaps = new Dictionary<TypePair, ITypeMap>();

        /// <summary>
        /// The list of profiles that contain mapping configurations.
        /// </summary>
        private readonly List<IProfileConfiguration> _profiles = new List<IProfileConfiguration>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MapperConfiguration"/> class.
        /// </summary>
        /// <param name="configure">The action to configure the mapping.</param>
        public MapperConfiguration(Action<IMapperConfigurationExpression> configure)
        {
            configure(this);
            InitializeProfiles();
        }

        /// <summary>
        /// Initializes the profiles by configuring each one and populating the type maps.
        /// </summary>
        private void InitializeProfiles()
        {
            foreach (var profile in _profiles)
            {
                profile.Configure();
                foreach (var typeMap in profile.TypeMaps)
                {
                    _typeMaps[new TypePair(typeMap.SourceType, typeMap.DestinationType)] = typeMap;
                }
            }
        }

        /// <inheritdoc />
        public ITypeMap FindTypeMapFor(Type sourceType, Type destinationType)
        {
            var key = new TypePair(sourceType, destinationType);
            _typeMaps.TryGetValue(key, out var typeMap);
            return typeMap;
        }

        /// <inheritdoc />
        public void AssertConfigurationIsValid()
        {
            // Basic validation can be added here
            if (!_typeMaps.Any())
            {
                throw new InvalidOperationException("No mapping configurations have been added");
            }
        }

        /// <summary>
        /// Creates a new instance of the Mapper using the current configuration.
        /// </summary>
        /// <returns>The created Mapper instance.</returns>
        public IMapper CreateMapper()
        {
            return new Mapper(this);
        }

        /// <inheritdoc />
        public void AddProfile(Profile profile)
        {
            _profiles.Add(profile);
            profile.Configure(this);
        }

        /// <inheritdoc />
        public void AddProfile<TProfile>() where TProfile : Profile, new()
        {
            AddProfile(new TProfile());
        }

        /// <inheritdoc />
        public IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>()
        {
            var typeMap = new TypeMap(typeof(TSource), typeof(TDestination));
            var typePair = new TypePair(typeof(TSource), typeof(TDestination));
            _typeMaps[typePair] = typeMap;
            return new MappingExpression<TSource, TDestination>(typeMap, this);
        }

        /// <inheritdoc />
        public IMappingExpression CreateMap(Type sourceType, Type destinationType)
        {
            var typeMap = new TypeMap(sourceType, destinationType);
            var typePair = new TypePair(sourceType, destinationType);
            _typeMaps[typePair] = typeMap;
            return new MappingExpression(typeMap, this);
        }

        /// <summary>
        /// Registers a type map for the specified source and destination types.
        /// </summary>
        /// <param name="typePair">The type pair representing the source and destination types.</param>
        /// <param name="typeMap">The type map to register.</param>
        internal void RegisterTypeMap(TypePair typePair, ITypeMap typeMap)
        {
            _typeMaps[typePair] = typeMap;
        }
    }

    /// <summary>
    /// Represents a pair of types used for mapping.
    /// </summary>
    internal readonly struct TypePair : IEquatable<TypePair>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypePair"/> struct.
        /// </summary>
        /// <param name="sourceType">The source type.</param>
        /// <param name="destinationType">The destination type.</param>
        public TypePair(Type sourceType, Type destinationType)
        {
            SourceType = sourceType;
            DestinationType = destinationType;
        }

        /// <summary>
        /// Gets the source type of the mapping.
        /// </summary>
        public Type SourceType { get; }

        /// <summary>
        /// Gets the destination type of the mapping.
        /// </summary>
        public Type DestinationType { get; }

        /// <inheritdoc />  
        public override bool Equals(object? obj) => obj is TypePair other && Equals(other);

        public bool Equals(TypePair other) =>
            SourceType == other.SourceType &&
            DestinationType == other.DestinationType;

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return SourceType.GetHashCode() * 397 ^ DestinationType.GetHashCode();
            }
        }
    }
}