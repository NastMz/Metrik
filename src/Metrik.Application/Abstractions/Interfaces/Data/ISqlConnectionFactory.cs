using System.Data;

namespace Metrik.Application.Abstractions.Interfaces.Data
{
    /// <summary>
    /// Interface for creating SQL database connections.
    /// </summary>
    public interface ISqlConnectionFactory
    {
        /// <summary>
        /// Creates a new SQL database connection.
        /// </summary>
        /// <returns>An instance of <see cref="IDbConnection"/> representing the SQL database connection.</returns>
        IDbConnection CreateConnection();
    }
}
