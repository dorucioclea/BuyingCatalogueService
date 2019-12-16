using Newtonsoft.Json;

namespace NHSD.BuyingCatalogue.Solutions.API.ViewModels
{
    public class NativeMobileResult
    {
        [JsonProperty("sections")]
        public NativeMobileSections NativeMobileSections { get; }

        public NativeMobileResult()
        {
            NativeMobileSections = new NativeMobileSections();
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

        public NativeMobileSections()
        {
            MobileOperatingSystems = new DashboardSection(true, false);
            MobileFirst = new DashboardSection(true, false);
            MobileMemoryStorage = new DashboardSection(true, false);
            MobileConnectionDetails = new DashboardSection(false, false);
            MobileComponentsDeviceCapabilities = new DashboardSection(false, false);
            MobileHardwareRequirements = new DashboardSection(false, false);
            MobileAdditionalInformation = new DashboardSection(false, false);
        }
    }
}