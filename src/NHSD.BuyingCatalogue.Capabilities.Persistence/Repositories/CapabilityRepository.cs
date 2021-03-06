using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NHSD.BuyingCatalogue.Capabilities.Contracts.Persistence;
using NHSD.BuyingCatalogue.Capabilities.Persistence.Models;
using NHSD.BuyingCatalogue.Data.Infrastructure;

namespace NHSD.BuyingCatalogue.Capabilities.Persistence.Repositories
{
    /// <summary>
    /// Represents the data access layer for the <see cref="ICapabilityListResult"/> entity.
    /// </summary>
    public sealed class CapabilityRepository : ICapabilityRepository
    {
        private readonly IDbConnector _dbConnector;

		public CapabilityRepository(IDbConnector dbConnector) => _dbConnector = dbConnector;

        private const string sql = @"SELECT Capability.Id, 
											 Name, 
											 ISNULL(IsFoundation, 0) AS IsFoundation
									FROM	 Capability 
											 LEFT OUTER JOIN FrameworkCapabilities ON Capability.Id = FrameworkCapabilities.CapabilityId
                                    ORDER BY IsFoundation DESC, UPPER(Name) ASC";

        /// <summary>
        /// Gets a list of <see cref="ICapabilityListResult"/> objects.
        /// </summary>
        /// <returns>A task representing an operation to retrieve a list of <see cref="ICapabilityListResult"/> objects.</returns>
        public async Task<IEnumerable<ICapabilityListResult>> ListAsync(CancellationToken cancellationToken)
            => await _dbConnector.QueryAsync<CapabilityListResult>(sql, cancellationToken).ConfigureAwait(false);
    }
}
