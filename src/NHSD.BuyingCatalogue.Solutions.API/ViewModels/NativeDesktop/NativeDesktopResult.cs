using Newtonsoft.Json;

namespace NHSD.BuyingCatalogue.Solutions.API.ViewModels.NativeDesktop
{
    public sealed class NativeDesktopResult
    {
        [JsonProperty("sections")]
        public NativeDesktopSections NativeDesktopSections { get; }

        public NativeDesktopResult()
        {
            NativeDesktopSections = new NativeDesktopSections();
        }
    }

    public sealed class NativeDesktopSections
    {
        [JsonProperty("native-desktop-operating-systems")]
        public DashboardSection OperatingSystems { get; }

        [JsonProperty("native-desktop-connection-details")]
        public DashboardSection ConnectionDetails { get; }

        [JsonProperty("native-desktop-memory-and-storage")]
        public DashboardSection MemoryAndStorage { get; }

        [JsonProperty("native-desktop-third-party")]
        public DashboardSection ThirdParty { get; }
        
        [JsonProperty("native-desktop-hardware-requirements")]
        public DashboardSection HardwareRequirements { get; }

        [JsonProperty("native-desktop-additional-information")]
        public DashboardSection AdditionalInformation { get; }


        public NativeDesktopSections()
        {
            OperatingSystems = DashboardSection.Mandatory(false);
            ConnectionDetails = DashboardSection.Mandatory(false);
            MemoryAndStorage = DashboardSection.Mandatory(false);
            ThirdParty = DashboardSection.Optional(false);
            HardwareRequirements = DashboardSection.Optional(false);
            AdditionalInformation = DashboardSection.Optional(false);
        }
    }
}