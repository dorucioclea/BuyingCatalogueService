using Newtonsoft.Json;
using NHSD.BuyingCatalogue.Solutions.Contracts;

namespace NHSD.BuyingCatalogue.Solutions.API.ViewModels.NativeMobile
{
    public class NativeMobileResult
    {
        [JsonProperty("sections")]
        public NativeMobileSections NativeMobileSections { get; }

        public NativeMobileResult(IClientApplication clientApplication)
        {
            NativeMobileSections = new NativeMobileSections(clientApplication);
        }
    }

    public class NativeMobileSections
    {
        [JsonProperty("mobile-operating-systems")]
        public DashboardSection MobileOperatingSystems { get; }

        [JsonProperty("mobile-first")]
        public DashboardSection MobileFirst { get; }
        
        [JsonProperty("mobile-memory-and-storage")]
        public DashboardSection MobileMemoryStorage { get; }

        [JsonProperty("mobile-connection-details")]
        public DashboardSection MobileConnectionDetails { get; }

        [JsonProperty("mobile-components-and-device-capabilities")]
        public DashboardSection MobileComponentsDeviceCapabilities { get; }

        [JsonProperty("mobile-hardware-requirements")]
        public DashboardSection MobileHardwareRequirements { get; }

        [JsonProperty("mobile-additional-information")]
        public DashboardSection MobileAdditionalInformation { get; }

        public NativeMobileSections(IClientApplication clientApplication)
        {
            MobileOperatingSystems = DashboardSection.Mandatory(clientApplication.IsMobileOperatingSystems());
            MobileFirst = DashboardSection.Mandatory(clientApplication.IsNativeMobileFirstComplete());
            MobileMemoryStorage = DashboardSection.Mandatory(clientApplication.IsMobileMemoryAndStorageComplete());
            MobileConnectionDetails = DashboardSection.Optional(clientApplication.IsMobileConnectionDetailsComplete());
            MobileComponentsDeviceCapabilities = DashboardSection.Optional(false);
            MobileHardwareRequirements = DashboardSection.Optional(false);
            MobileAdditionalInformation = DashboardSection.Optional(false);
        }
    }
}
