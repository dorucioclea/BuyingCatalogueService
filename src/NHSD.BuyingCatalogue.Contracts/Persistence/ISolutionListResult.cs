using System;

namespace NHSD.BuyingCatalogue.Contracts.Persistence
{
    public interface ISolutionListResult
    {
        string SolutionId { get; }

        string SolutionName { get; }

        string LastUpdated { get; }

        string SolutionSummary { get; }

        Guid OrganisationId { get; }

        string OrganisationName { get; }

        Guid CapabilityId { get; }

        string CapabilityName { get; }

        string CapabilityDescription { get; }

        bool IsFoundation { get; }
    }
}
