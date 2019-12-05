using System.Collections.Generic;

namespace NHSD.BuyingCatalogue.Solutions.Contracts
{
    public interface IClientApplication
    {
        HashSet<string> ClientApplicationTypes { get; }
        HashSet<string> BrowsersSupported { get; }
        bool? MobileResponsive { get; }
        IPlugins Plugins { get; }
    }
}