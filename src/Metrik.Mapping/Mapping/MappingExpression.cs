using Metrik.Mapping.Configuration;
using Metrik.Mapping.MemberConfiguration;
using System.Linq.Expressions;

namespace Metrik.Mapping.Mapping
{
    /// <summary>
    /// Represents a mapping expression for configuring mappings between source and destination types.
    /// </summary>
    internal class MappingExpression : IMappingExpression
    {
        /// <summary>
        /// The type map associated with this mapping expression.
        /// </summary>
        private readonly ITypeMap _typeMap;

        /// <summary>
        /// The configuration provider for the mapping expression.
        /// </summary>
        private readonly MapperConfiguration _configurationProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingExpression"/> class.
        /// </summary>
        /// <param name="typeMap">The type map associated with this mapping expression.</param>
        /// <param name="configurationProvider">The configuration provider for the mapping expression.</param>
        public MappingExpression(ITypeMap typeMap, MapperConfiguration configurationProvider)
        {
            _typeMap = typeMap;
            _configurationProvider = configurationProvider;
        }

        /// <inheritdoc />
        public IMappingExpression ForMember(string name, Action<IMemberConfigurationExpression> memberOptions)
        {
            var memberConfig = new MemberConfigurationExpression(_typeMap, name);
            memberOptions(memberConfig);
            return this;
        }

        /// <inheritdoc />
        public IMappingExpression ReverseMap()
        {
            // Create the reverse mapping
            var reverseTypeMap = new TypeMap(_typeMap.DestinationType, _typeMap.SourceType);

            // Add the reverse mapping to the configuration
            var typePair = new TypePair(_typeMap.DestinationType, _typeMap.SourceType);
            _configurationProvider.RegisterTypeMap(typePair, reverseTypeMap);

            // Return a mapping expression for the reverse mapping
            return new MappingExpression(reverseTypeMap, _configurationProvider);
        }
    }

    /// <summary>
    /// Represents a mapping expression for configuring mappings between source and destination types with specific types.
    /// </summary>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    internal class MappingExpression<TSource, TDestination> : IMappingExpression<TSource, TDestination>
    {
        /// <summary>
        /// The type map associated with this mapping expression.
        /// </summary>
        private readonly ITypeMap _typeMap;

        /// <summary>
        /// The configuration provider for the mapping expression.
        /// </summary>
        private readonly MapperConfiguration _configurationProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingExpression{TSource, TDestination}"/> class.
        /// </summary>
        /// <param name="typeMap">The type map associated with this mapping expression.</param>
        /// <param name="configurationProvider">The configuration provider for the mapping expression.</param>
        public MappingExpression(ITypeMap typeMap, MapperConfiguration configurationProvider)
        {
            _typeMap = typeMap;
            _configurationProvider = configurationProvider;
        }

        /// <inheritdoc />
        public IMappingExpression ForMember(string name, Action<IMemberConfigurationExpression> memberOptions)
        {
            var memberConfig = new MemberConfigurationExpression(_typeMap, name);
            memberOptions(memberConfig);
            return this;
        }

        /// <inheritdoc />
        public IMappingExpression<TSource, TDestination> ForMember<TMember>(
            Expression<Func<TDestination, TMember>> destinationMember,
            Action<IMemberConfigurationExpression<TSource, TDestination, TMember>> memberOptions)
        {
            if (destinationMember.Body is MemberExpression memberExpr)
            {
                var memberConfig = new MemberConfigurationExpression<TSource, TDestination, TMember>(_typeMap, memberExpr.Member.Name);
                memberOptions(memberConfig);
            }
            return this;
        }

        /// <inheritdoc />
        IMappingExpression IMappingExpression.ReverseMap()
        {
            return CreateReverseMap();
        }

        /// <inheritdoc />
        public IMappingExpression<TDestination, TSource> ReverseMap()
        {
            var reverseMap = CreateReverseMap();
            return (IMappingExpression<TDestination, TSource>)reverseMap;
        }

        /// <summary>
        /// Creates a reverse mapping expression for the current mapping expression.
        /// </summary>
        /// <returns>The reverse mapping expression.</returns>
        private IMappingExpression CreateReverseMap()
        {
            // Create the reverse mapping
            var reverseTypeMap = new TypeMap(typeof(TDestination), typeof(TSource));

            // Add the reverse mapping to the configuration
            var typePair = new TypePair(typeof(TDestination), typeof(TSource));
            _configurationProvider.RegisterTypeMap(typePair, reverseTypeMap);

            // Return a mapping expression for the reverse mapping
            return new MappingExpression<TDestination, TSource>(reverseTypeMap, _configurationProvider);
        }
    }
}