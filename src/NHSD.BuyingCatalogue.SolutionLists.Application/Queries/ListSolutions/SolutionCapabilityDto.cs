using System;
using NHSD.BuyingCatalogue.SolutionLists.Contracts;

namespace NHSD.BuyingCatalogue.SolutionLists.Application.Queries.ListSolutions
{
    /// <summary>
    /// Provides the view representation for the <see cref="Capability"/> entity.
    /// </summary>
    internal sealed class SolutionCapabilityDto : ISolutionCapability
    {
        /// <summary>
        /// Identifier of the capability.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the capability.
        /// </summary>
        public string Name { get; set; }
    }
}
