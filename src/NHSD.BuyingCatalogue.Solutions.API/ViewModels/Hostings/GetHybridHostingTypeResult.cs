using System.Collections.Generic;
using Newtonsoft.Json;
using NHSD.BuyingCatalogue.Solutions.Contracts.Hostings;

namespace NHSD.BuyingCatalogue.Solutions.API.ViewModels.Hostings
{
    public sealed class GetHybridHostingTypeResult
    {
        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("hosting-model")]
        public string HostingModel { get; set; }

        [JsonProperty("requires-hscn")]
        public HashSet<string> RequiresHSCN { get; }

        public GetHybridHostingTypeResult(IHybridHostingType hybridHostingType)
        {
            Summary = hybridHostingType?.Summary;
            Link = hybridHostingType?.Link;
            HostingModel = hybridHostingType?.HostingModel;
            RequiresHSCN = hybridHostingType?.RequiresHSCN != null
                ? new HashSet<string> {hybridHostingType?.RequiresHSCN}
                : new HashSet<string>();
        }
    }
}
