using Metrik.Mapping.Mapping;
using System.Linq.Expressions;

namespace Metrik.Mapping.MemberConfiguration
{
    /// <summary>
    /// Represents a member configuration expression for configuring mappings between source and destination members.
    /// </summary>
    internal sealed class MemberConfigurationExpression : IMemberConfigurationExpression
    {
        /// <summary>
        /// The type map associated with this member configuration.
        /// </summary>
        private readonly ITypeMap _typeMap;

        /// <summary>
        /// The name of the destination member being configured.
        /// </summary>
        private readonly string _destinationMemberName;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberConfigurationExpression"/> class.
        /// </summary>
        /// <param name="typeMap">The type map associated with this member configuration.</param>
        /// <param name="destinationMemberName">The name of the destination member being configured.</param>
        public MemberConfigurationExpression(ITypeMap typeMap, string destinationMemberName)
        {
            _typeMap = typeMap;
            _destinationMemberName = destinationMemberName;
        }

        /// <inheritdoc />
        public void MapFrom(string sourceMemberName)
        {
            // Configure source member mapping
            ((TypeMap)_typeMap).AddMemberMapping(_destinationMemberName, sourceMemberName);
        }

        /// <inheritdoc />
        public void Ignore()
        {
            // Mark member to be ignored during mapping
            ((TypeMap)_typeMap).IgnoreMember(_destinationMemberName);
        }

        /// <inheritdoc />
        public void UseValue(object value)
        {
            // Configure constant value for the member
            ((TypeMap)_typeMap).AddValueMapping(_destinationMemberName, value);
        }
    }

    /// <summary>
    /// Represents a member configuration expression for configuring mappings between source and destination members with specific types.
    /// </summary>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TDestination">The destination type.</typeparam>
    /// <typeparam name="TMember">The member type.</typeparam>
    internal sealed class MemberConfigurationExpression<TSource, TDestination, TMember> :
        IMemberConfigurationExpression<TSource, TDestination, TMember>
    {
        /// <summary>
        /// The type map associated with this member configuration.
        /// </summary>
        private readonly ITypeMap _typeMap;

        /// <summary>
        /// The name of the destination member being configured.
        /// </summary>
        private readonly string _destinationMemberName;


        /// <summary>
        /// Initializes a new instance of the <see cref="MemberConfigurationExpression{TSource, TDestination, TMember}"/> class.
        /// </summary>
        /// <param name="typeMap">The type map associated with this member configuration.</param>
        /// <param name="destinationMemberName">The name of the destination member being configured.</param>
        public MemberConfigurationExpression(ITypeMap typeMap, string destinationMemberName)
        {
            _typeMap = typeMap;
            _destinationMemberName = destinationMemberName;
        }

        /// <inheritdoc />
        public void MapFrom<TSourceMember>(Expression<Func<TSource, TSourceMember>> sourceMember)
        {
            if (sourceMember.Body is MemberExpression memberExpr)
            {
                // Configure expression based mapping
                ((TypeMap)_typeMap).AddMemberMapping(_destinationMemberName, memberExpr.Member.Name);
            }
        }

        /// <inheritdoc />
        public void MapFrom(Func<TSource, TMember> sourceMember)
        {
            // Configure function-based mapping
            ((TypeMap)_typeMap).AddFunctionMapping(_destinationMemberName, source => sourceMember((TSource)source));
        }

        /// <inheritdoc />
        public void Ignore()
        {
            // Mark member to be ignored during mapping
            ((TypeMap)_typeMap).IgnoreMember(_destinationMemberName);
        }

        /// <inheritdoc />
        public void UseValue(TMember value)
        {
            // Configure constant value for the member
            ((TypeMap)_typeMap).AddValueMapping(_destinationMemberName, value);
        }
    }
}