using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using NHSD.BuyingCatalogue.Solutions.Contracts;

namespace NHSD.BuyingCatalogue.Solutions.API.ViewModels.Solution
{
    public class MobileOperatingSystemsSectionAnswers
    {
        [JsonProperty("operating-systems")]
        public IEnumerable<string> OperatingSystems { get; }

        [JsonProperty("operating-systems-description")]
        public string OperatingSystemsDescription { get; }

        [JsonIgnore] public bool HasData => OperatingSystems?.Any() == true;

        public MobileOperatingSystemsSectionAnswers(IClientApplication clientApplication)
        {
            OperatingSystems = clientApplication?.MobileOperatingSystems?.OperatingSystems?.Any() == true
                ? clientApplication?.MobileOperatingSystems?.OperatingSystems : null;
            OperatingSystemsDescription = clientApplication?.MobileOperatingSystems?.OperatingSystemsDescription;
        }
    }
}
