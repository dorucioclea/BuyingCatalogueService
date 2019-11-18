using AutoMapper;
using NHSD.BuyingCatalogue.Application.Capabilities.Queries.ListCapabilities;
using NHSD.BuyingCatalogue.Application.SolutionList.Domain;
using NHSD.BuyingCatalogue.Application.SolutionList.Queries.ListSolutions;
using NHSD.BuyingCatalogue.Application.Solutions.Commands.UpdateSolutionSummary;
using NHSD.BuyingCatalogue.Application.Solutions.Commands.UpdateSolutionFeatures;
using NHSD.BuyingCatalogue.Application.Capabilities.Domain;
using NHSD.BuyingCatalogue.Application.Solutions.Domain;
using NHSD.BuyingCatalogue.Application.Solutions.Queries.GetSolutionById;
using NHSD.BuyingCatalogue.Contracts;
using NHSD.BuyingCatalogue.Contracts.SolutionList;
using NHSD.BuyingCatalogue.Contracts.Solutions;

namespace NHSD.BuyingCatalogue.Application.Infrastructure.Mapping
{
    /// <summary>
    /// A profile for AutoMapper to define the mapping between entities and view models.
    /// </summary>
    public sealed class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AutoMapperProfile"/> class.
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<SolutionList.Domain.SolutionList, SolutionListDto>();
            CreateMap<SolutionList.Domain.SolutionList, ISolutionList>().As<SolutionListDto>();
            CreateMap<SolutionListItem, SolutionSummaryDto>();
            CreateMap<SolutionListItem, ISolutionSummary>().As<SolutionSummaryDto>();
            CreateMap<SolutionListItemCapability, SolutionCapabilityDto>();
            CreateMap<SolutionListItemCapability, ISolutionCapability>().As<SolutionCapabilityDto>();
            CreateMap<SolutionListItemOrganisation, SolutionOrganisationDto>();
            CreateMap<SolutionListItemOrganisation, ISolutionOrganisation>().As<SolutionOrganisationDto>();

            CreateMap<Capability, CapabilityDto>();

            CreateMap<UpdateSolutionSummaryViewModel, Solution>()
                .ForMember(destination => destination.AboutUrl, options => options.MapFrom(source => source.Link));
            CreateMap<UpdateSolutionFeaturesViewModel, Solution>()
                .ForMember(destination => destination.Features, options => options.MapFrom(source => source.Listing));

            CreateMap<Solution, SolutionDto>();
            CreateMap<Solution, ISolution>().As<SolutionDto>();
            CreateMap<ClientApplication, ClientApplicationDto>();
            CreateMap<ClientApplication, IClientApplication>().As<ClientApplicationDto>();
            CreateMap<Plugins, PluginsDto>();
            CreateMap<Plugins, IPlugins>().As<PluginsDto>();
            CreateMap<Contact, ContactDto>();
            CreateMap<Contact, IContact>().As<ContactDto>();
        }
    }
}
