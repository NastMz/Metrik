using Dapper;
using System.Data;

namespace Metrik.Infrastructure.Data
{
    /// <summary>
    /// Custom Dapper type handler for <see cref="DateOnly"/> to handle date-only values in PostgreSQL.
    /// </summary>
    internal sealed class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
    {
        /// <inheritdoc />
        public override DateOnly Parse(object value) => DateOnly.FromDateTime((DateTime)value);

        /// <inheritdoc />
        public override void SetValue(IDbDataParameter parameter, DateOnly value)
        {
            parameter.DbType = DbType.Date;
            parameter.Value = value;
        }
    }
}
