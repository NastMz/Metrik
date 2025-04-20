using System.Linq.Expressions;

namespace Metrik.Mapping.MemberConfiguration
{
    /// <summary>
    /// Interface for configuring member mappings
    /// </summary>
    public interface IMemberConfigurationExpression
    {
        /// <summary>
        /// Maps a member from the source type to the destination type
        /// </summary>
        /// <param name="sourceMemberName">The name of the source member</param>
        void MapFrom(string sourceMemberName);

        /// <summary>
        /// Ignores the member during mapping
        /// </summary>
        void Ignore();

        /// <summary>
        /// Uses a constant value for the member during mapping
        /// </summary>
        /// <param name="value">The constant value to use</param>
        void UseValue(object value);
    }

    /// <summary>
    /// Interface for configuring member mappings with source and destination types
    /// </summary>
    /// <typeparam name="TSource">The source type</typeparam>
    /// <typeparam name="TDestination">The destination type</typeparam>
    /// <typeparam name="TMember">The member type</typeparam>
    public interface IMemberConfigurationExpression<TSource, TDestination, TMember>
    {
        /// <summary>
        /// Maps a member from the source type to the destination type using an expression
        /// </summary>
        /// <typeparam name="TSourceMember">The type of the source member</typeparam>
        /// <param name="sourceMember">The expression representing the source member</param>
        void MapFrom<TSourceMember>(Expression<Func<TSource, TSourceMember>> sourceMember);

        /// <summary>
        /// Maps a member from the source type to the destination type using a function
        /// </summary>
        /// <param name="sourceMember">The function representing the source member</param>
        void MapFrom(Func<TSource, TMember> sourceMember);

        /// <summary>
        /// Ignores the member during mapping
        /// </summary>
        void Ignore();

        /// <summary>
        /// Uses a constant value for the member during mapping
        /// </summary>
        /// <param name="value">The constant value to use</param>
        void UseValue(TMember value);
    }
}