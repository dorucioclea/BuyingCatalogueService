using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Newtonsoft.Json.Linq;
using NHSD.BuyingCatalogue.Application.Exceptions;
using NHSD.BuyingCatalogue.Application.Solutions.Commands.UpdateSolution;
using NHSD.BuyingCatalogue.Application.UnitTests.Tools;
using NHSD.BuyingCatalogue.Contracts.Persistence;
using NUnit.Framework;
namespace NHSD.BuyingCatalogue.Application.UnitTests.Solutions
{
    [TestFixture]
    internal sealed class SolutionUpdateClientApplicationTypesTests : ClientApplicationTestsBase
    {

        [Test]
        public async Task ShouldUpdateSolutionClientApplicationTypes()
        {
            SetUpMockSolutionRepositoryGetByIdAsync("{}");

            await UpdateClientApplicationTypes(new HashSet<string> { "browser-based", "native-mobile", });

            Context.MockSolutionRepository.Verify(r => r.ByIdAsync("Sln1", It.IsAny<CancellationToken>()), Times.Once());

            Context.MockMarketingDetailRepository.Verify(r => r.UpdateClientApplicationAsync(It.Is<IUpdateSolutionClientApplicationRequest>(r =>
                r.Id == "Sln1"
                && JToken.Parse(r.ClientApplication).ReadStringArray("ClientApplicationTypes").ShouldContainOnly(new List<string> { "browser-based", "native-mobile" }).Count() == 2
            ), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public async Task ShouldUpdateSolutionClientApplicationTypesAndNothingElse()
        {
            SetUpMockSolutionRepositoryGetByIdAsync("{ 'ClientApplicationTypes' : [ 'browser-based', 'native-mobile' ], 'BrowsersSupported' : [ 'Chrome', 'Edge' ], 'MobileResponsive': true }");
            var calledBack = SetUpCallback((jsonString) =>
            {
                var json = JToken.Parse(jsonString);

                json.ReadStringArray("ClientApplicationTypes")
                    .ShouldContainOnly(new List<string> { "native-mobile", "native-desktop" })
                    .ShouldNotContain("browser-based");
                json.ReadStringArray("BrowsersSupported")
                    .ShouldContainOnly(new List<string> { "Chrome", "Edge" });
                json.SelectToken("MobileResponsive").Value<bool>()
                    .Should().BeTrue();
            });

            await UpdateClientApplicationTypes(new HashSet<string> { "native-desktop", "native-mobile" });

            Context.MockSolutionRepository.Verify(r => r.ByIdAsync("Sln1", It.IsAny<CancellationToken>()), Times.Once());

            calledBack.Should().BeTrue();
        }

        [Test]
        public async Task ShouldUpdateEmptySolutionClientApplicationTypes()
        {
            SetUpMockSolutionRepositoryGetByIdAsync("{}");

            await UpdateClientApplicationTypes(new HashSet<string>());

            Context.MockSolutionRepository.Verify(r => r.ByIdAsync("Sln1", It.IsAny<CancellationToken>()), Times.Once());

            Context.MockMarketingDetailRepository.Verify(r => r.UpdateClientApplicationAsync(It.Is<IUpdateSolutionClientApplicationRequest>(r =>
                r.Id == "Sln1"
                && !JToken.Parse(r.ClientApplication).ReadStringArray("ClientApplicationTypes").Any()
            ), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public async Task ShouldUpdateEmptySolutionClientApplicationTypesAndNothingElse()
        {
            SetUpMockSolutionRepositoryGetByIdAsync("{ 'ClientApplicationTypes' : [ 'browser-based', 'native-mobile' ], 'BrowsersSupported' : [ 'Chrome', 'Edge' ], 'MobileResponsive': true }");
            var calledBack = SetUpCallback((jsonString) =>
            {
                var json = JToken.Parse(jsonString);

                json.ReadStringArray("ClientApplicationTypes").Should().BeEmpty();
                json.ReadStringArray("BrowsersSupported").ShouldContainOnly(new List<string> { "Chrome", "Edge" });
                json.SelectToken("MobileResponsive").Value<bool>().Should().BeTrue();
            });

            await UpdateClientApplicationTypes(new HashSet<string>());

            Context.MockSolutionRepository.Verify(r => r.ByIdAsync("Sln1", It.IsAny<CancellationToken>()), Times.Once());

            calledBack.Should().BeTrue();
        }

        [Test]
        public async Task ShouldIgnoreUnknownClientApplicationTypes()
        {
            SetUpMockSolutionRepositoryGetByIdAsync("{}");

            await UpdateClientApplicationTypes(new HashSet<string> { "browser-based", "curry", "native-mobile", "native-desktop", "elephant", "anteater", "blue", null, "" });

            Context.MockSolutionRepository.Verify(r => r.ByIdAsync("Sln1", It.IsAny<CancellationToken>()), Times.Once());

            Context.MockMarketingDetailRepository.Verify(r => r.UpdateClientApplicationAsync(It.Is<IUpdateSolutionClientApplicationRequest>(r =>
                r.Id == "Sln1"
                && JToken.Parse(r.ClientApplication).ReadStringArray("ClientApplicationTypes").ShouldContainOnly(new List<string> { "browser-based", "native-mobile", "native-desktop" }).Count() == 3
            ), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Test]
        public async Task ShouldIgnoreUnknownClientApplicationTypesAndNotChangeAnythingElse()
        {
            SetUpMockSolutionRepositoryGetByIdAsync("{ 'ClientApplicationTypes' : [ 'browser-based', 'native-mobile' ], 'BrowsersSupported' : [ 'Chrome', 'Edge' ], 'MobileResponsive': true }");

            var calledBack = SetUpCallback((jsonString) =>
            {
                var json = JToken.Parse(jsonString);

                json.ReadStringArray("ClientApplicationTypes").ShouldContainOnly(new List<string> { "native-mobile", "native-desktop", "browser-based" });
                json.ReadStringArray("BrowsersSupported").ShouldContainOnly(new List<string> { "Chrome", "Edge" });
                json.SelectToken("MobileResponsive").Value<bool>().Should().BeTrue();
            });

            await UpdateClientApplicationTypes(new HashSet<string> { "browser-based", "curry", "native-mobile", "native-desktop", "elephant", "anteater", "blue", null, "" });

            Context.MockSolutionRepository.Verify(r => r.ByIdAsync("Sln1", It.IsAny<CancellationToken>()), Times.Once());

            calledBack.Should().BeTrue();
        }

        [Test]
        public async Task ShouldIgnoreUnknownClientApplicationTypesAndBrowsersSupportedRemainEmpty()
        {
            SetUpMockSolutionRepositoryGetByIdAsync("{ 'ClientApplicationTypes' : [ 'browser-based', 'native-mobile' ], 'BrowsersSupported' : [ ]}");
            var calledBack = SetUpCallback((jsonString) =>
            {
                var json = JToken.Parse(jsonString);

                json.ReadStringArray("ClientApplicationTypes")
                    .ShouldContainOnly(new List<string> { "native-mobile", "native-desktop", "browser-based" })
                    .ShouldNotContainAnyOf(new List<string> { "curry", "elephant", "anteater", "blue", "" });
                json.ReadStringArray("BrowsersSupported").Should().BeEmpty();
                json.SelectToken("MobileResponsive").Should().BeNullOrEmpty();
            });

            await UpdateClientApplicationTypes(new HashSet<string> { "browser-based", "curry", "native-mobile", "native-desktop", "elephant", "anteater", "blue", null, "" });

            Context.MockSolutionRepository.Verify(r => r.ByIdAsync("Sln1", It.IsAny<CancellationToken>()), Times.Once());

            calledBack.Should().BeTrue();
        }

        [Test]
        public void ShouldThrowWhenSolutionNotPresent()
        {
            Assert.ThrowsAsync<NotFoundException>(() =>
                UpdateClientApplicationTypes(new HashSet<string> { "browser-based", "native-mobile" }));

            Context.MockSolutionRepository.Verify(r => r.ByIdAsync("Sln1", It.IsAny<CancellationToken>()), Times.Once());

            Context.MockMarketingDetailRepository.Verify(r => r.UpdateClientApplicationAsync(It.IsAny<IUpdateSolutionClientApplicationRequest>(), It.IsAny<CancellationToken>()), Times.Never());
        }

        private async Task UpdateClientApplicationTypes(HashSet<string> clientApplicationTypes)
        {
            await Context.UpdateSolutionClientApplicationTypesHandler.Handle(new UpdateSolutionClientApplicationTypesCommand("Sln1",
                new UpdateSolutionClientApplicationTypesViewModel
                {
                    ClientApplicationTypes = clientApplicationTypes
                }), new CancellationToken());

        }
    }
}
