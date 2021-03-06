using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using NHSD.BuyingCatalogue.Contracts.Infrastructure;
using NHSD.BuyingCatalogue.Infrastructure;
namespace NHSD.BuyingCatalogue.Data.Infrastructure
{
    /// <summary>
    /// A factory to provide a new database connection.
    /// </summary>
    internal sealed class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly ISettings _settings;

        /// <summary>
        /// Initialises a new instance of the <see cref="DbConnectionFactory"/> class.
        /// </summary>
        public DbConnectionFactory(ISettings settings)
            => _settings = settings.ThrowIfNull(nameof(settings));

        /// <summary>
        /// Gets a new database connection.
        /// </summary>
        /// <returns>A new database connection.</returns>
        public async Task<IDbConnection> GetAsync(CancellationToken cancellationToken)
            => await GetAsync(new SqlConnectionStringBuilder(_settings.ConnectionString), cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Gets a new database connection.
        /// </summary>
        /// <returns>A new database connection.</returns>
        public async Task<IDbConnection> GetAsync(DbConnectionStringBuilder connectionStringBuilder, CancellationToken cancellationToken)
        {
            var connection = SqlClientFactory.Instance.CreateConnection();
            connection.ConnectionString = connectionStringBuilder.ThrowIfNull().ConnectionString;

            if (connection is SqlConnection sqlConnection)
            {
                sqlConnection.StatisticsEnabled = true;
            }

            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

            return connection;
        }
    }
}
