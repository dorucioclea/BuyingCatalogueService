using System.Collections.Generic;

namespace NHSD.BuyingCatalogue.Solutions.Contracts.Commands.Hostings
{
    public interface IUpdatePublicCloudData
    {
        string Summary { get; }

        string Link { get; }

        HashSet<string> RequiresHSCN { get; }
    }
}
