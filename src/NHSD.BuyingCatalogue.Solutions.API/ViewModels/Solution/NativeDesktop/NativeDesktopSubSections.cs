using Newtonsoft.Json;
using NHSD.BuyingCatalogue.Solutions.Contracts;

namespace NHSD.BuyingCatalogue.Solutions.API.ViewModels.Solution.NativeDesktop
{
    public sealed class NativeDesktopSubSections
    {
       [JsonProperty("native-desktop-hardware-requirements")]
       public NativeDesktopHardwareRequirementsSection HardwareRequirementsSection { get; }

       [JsonProperty("native-desktop-operating-systems-description")]
       public NativeDesktopOperatingSystemsSection OperatingSystemsSection { get; }

        [JsonIgnore]
       public bool HasData => HardwareRequirementsSection.Answers.HasData
                              || OperatingSystemsSection.Answers.HasData;

       internal NativeDesktopSubSections(IClientApplication clientApplication)
       {
           HardwareRequirementsSection = new NativeDesktopHardwareRequirementsSection(clientApplication);
           OperatingSystemsSection = new NativeDesktopOperatingSystemsSection(clientApplication);
       }
    }
}
