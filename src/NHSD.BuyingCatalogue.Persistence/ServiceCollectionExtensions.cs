using Microsoft.Extensions.DependencyInjection;
using NHSD.BuyingCatalogue.Contracts.Persistence;
using NHSD.BuyingCatalogue.Persistence.Repositories;

namespace NHSD.BuyingCatalogue.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterPersistence(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ICapabilityRepository, CapabilityRepository>();
            serviceCollection.AddTransient<ISolutionRepository, SolutionRepository>();
            return serviceCollection;
        }
    }
}