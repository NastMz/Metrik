using Metrik.Application.Abstractions.Interfaces.Data;
using Npgsql;
using System.Data;

namespace Metrik.Infrastructure.Data
{
    /// <summary>
    /// Factory class for creating SQL connections.
    /// </summary>
    internal sealed class SqlConnectionFactory : ISqlConnectionFactory
    {
        /// <summary>
        /// The connection string used to connect to the database.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlConnectionFactory"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string used to connect to the database.</param>
        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <inheritdoc />
        public IDbConnection CreateConnection()
        {
            var connection = new NpgsqlConnection(_connectionString);

            connection.Open();

            return connection;
        }
    }
}
