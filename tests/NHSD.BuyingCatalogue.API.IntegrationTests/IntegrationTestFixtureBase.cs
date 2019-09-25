using System.Threading.Tasks;
using NHSD.BuyingCatalogue.API.IntegrationTests.Drivers;
using NHSD.BuyingCatalogue.Testing.Data;
using NUnit.Framework;

namespace NHSD.BuyingCatalogue.API.IntegrationTests
{
    public abstract class IntegrationTestFixtureBase
    {
        [OneTimeSetUp]
        public async Task OneTimeSetUpAsync()
        {
            await BuyingCatalogueService.StartAsync();

            await ConnectionAwaiter.AwaitConnectionAsync("localhost", 30);
            await Database.Create();

            await BuyingCatalogueService.WaitAsync();
        }

        [SetUp]
        public async Task SetUpAsync()
        {
            await Database.Clear();
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDownAsync()
        {
            await Database.Drop();
            await BuyingCatalogueService.StopAsync();
        }
    }
}
