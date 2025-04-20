using Metrik.Mapping.MemberConfiguration;
using System.Linq.Expressions;

namespace Metrik.Mapping.Mapping
{
    /// <summary>
    /// Interface for configuring mapping expressions
    /// </summary>
    public interface IMappingExpression
    {
        /// <summary>
        /// Configures a member mapping
        /// </summary>
        /// <param name="name">The name of the member</param>
        /// <param name="memberOptions">The options for configuring the member</param>
        /// <returns>The mapping expression</returns>
        IMappingExpression ForMember(string name, Action<IMemberConfigurationExpression> memberOptions);

        /// <summary>
        /// Creates a reverse mapping expression
        /// </summary>
        /// <returns>The reverse mapping expression</returns>
        IMappingExpression ReverseMap();
    }

    /// <summary>
    /// Interface for configuring mapping expressions with source and destination types
    /// </summary>
    /// <typeparam name="TSource">The source type</typeparam>
    /// <typeparam name="TDestination">The destination type</typeparam>
    public interface IMappingExpression<TSource, TDestination> : IMappingExpression
    {
        /// <summary>
        /// Configures a member mapping for the destination type
        /// </summary>
        /// <typeparam name="TMember">The type of the member</typeparam>
        /// <param name="destinationMember">The expression representing the destination member</param>
        /// <param name="memberOptions">The options for configuring the member</param>
        /// <returns>The mapping expression</returns>
        IMappingExpression<TSource, TDestination> ForMember<TMember>(
            Expression<Func<TDestination, TMember>> destinationMember,
            Action<IMemberConfigurationExpression<TSource, TDestination, TMember>> memberOptions);

        /// <summary>
        /// Creates a reverse mapping expression
        /// </summary>
        /// <returns>The reverse mapping expression</returns>
        IMappingExpression<TDestination, TSource> ReverseMap();
    }
}