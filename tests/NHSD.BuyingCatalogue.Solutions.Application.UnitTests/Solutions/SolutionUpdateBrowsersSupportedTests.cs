using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Newtonsoft.Json.Linq;
using NHSD.BuyingCatalogue.Infrastructure.Exceptions;
using NHSD.BuyingCatalogue.Solutions.Application.Commands.UpdateSolutionBrowsersSupported;
using NHSD.BuyingCatalogue.Solutions.Application.Commands.Validation;
using NHSD.BuyingCatalogue.Solutions.Application.UnitTests.Tools;
using NHSD.BuyingCatalogue.Solutions.Contracts.Persistence;
using NUnit.Framework;

namespace NHSD.BuyingCatalogue.Solutions.Application.UnitTests.Solutions
{
    [TestFixture]
    internal sealed class SolutionUpdateBrowsersSupportedTests : ClientApplicationTestsBase
    {

        [Test]
        public async Task ShouldUpdateSolutionBrowsersSupported()
        {
            SetUpMockSolutionRepositoryGetByIdAsync("{}");

            var validationResult = await UpdateBrowsersSupported(new HashSet<string> { "Edge", "Google Chrome" }, "yes")
                .ConfigureAwait(false);
            validationResult.IsValid.Should().BeTrue();

            Context.MockSolutionRepository.Verify(r => r.ByIdAsync("Sln1", It.IsAny<CancellationToken>()), Times.Once());
            Context.MockSolutionDetailRepository.Verify(r => r.UpdateClientApplicationAsync(It.Is<IUpdateSolutionClientApplicationRequest>(r =>
                r.SolutionId == "Sln1"
                && JToken.Parse(r.ClientApplication).ReadStringArray("BrowsersSupported").ShouldContainOnly(new List<string> { "Edge", "Google Chrome" }).Count() == 2
                && JToken.Parse(r.ClientApplication).SelectToken("MobileResponsive").Value<bool>() == true
            ), It.IsAny<CancellationToken>()), Times.Once());

        }

        [Test]
        public async Task ShouldUpdateSolutionBrowsersSupportedAndNothingElse()
        {
            SetUpMockSolutionRepositoryGetByIdAsync("{ 'ClientApplicationTypes' : [ 'browser-based', 'native-mobile' ], 'BrowsersSupported' : [ 'Mozilla Firefox', 'Edge' ], 'MobileResponsive': false }");

            var calledBack = false;

            Context.MockSolutionDetailRepository
                .Setup(r => r.UpdateClientApplicationAsync(It.IsAny<IUpdateSolutionClientApplicationRequest>(), It.IsAny<CancellationToken>()))
                .Callback((IUpdateSolutionClientApplicationRequest updateSolutionClientApplicationRequest, CancellationToken cancellationToken) =>
                {
                    calledBack = true;
                    var json = JToken.Parse(updateSolutionClientApplicationRequest.ClientApplication);

                    json.ReadStringArray("ClientApplicationTypes").ShouldContainOnly(new List<string> { "native-mobile", "browser-based" });
                    json.ReadStringArray("BrowsersSupported").ShouldContainOnly(new List<string> { "Google Chrome", "Edge" });
                    json.SelectToken("MobileResponsive").Value<bool>().Should().BeTrue();
                });

            var validationResult = await UpdateBrowsersSupported(new HashSet<string> { "Edge", "Google Chrome" }, "yes")
                .ConfigureAwait(false);
            validationResult.IsValid.Should().BeTrue();

            Context.MockSolutionRepository.Verify(r => r.ByIdAsync("Sln1", It.IsAny<CancellationToken>()), Times.Once());

            calledBack.Should().BeTrue();
        }

        [Test]
        public async Task ShouldNotUpdateEmptySolutionBrowsersSupported()
        {
            SetUpMockSolutionRepositoryGetByIdAsync("{}");

            var validationResult = await UpdateBrowsersSupported(new HashSet<string>())
                .ConfigureAwait(false);
            validationResult.IsValid.Should().BeFalse();
            validationResult.Required.Should().BeEquivalentTo(new[] { "supported-browsers", "mobile-responsive" });

            Context.MockSolutionRepository.Verify(r => r.ByIdAsync("Sln1", It.IsAny<CancellationToken>()), Times.Never());

            Context.MockSolutionDetailRepository.Verify(
                r => r.UpdateClientApplicationAsync(It.IsAny<IUpdateSolutionClientApplicationRequest>(),
                    It.IsAny<CancellationToken>()), Times.Never());
        }

        [Test]
        public void ShouldThrowWhenSolutionNotPresent()
        {
            Assert.ThrowsAsync<NotFoundException>(() => UpdateBrowsersSupported(new HashSet<string> { "Edge", "Google Chrome" }, "yes"));

            Context.MockSolutionRepository.Verify(r => r.ByIdAsync("Sln1", It.IsAny<CancellationToken>()), Times.Once());

            Context.MockSolutionDetailRepository.Verify(r => r.UpdateClientApplicationAsync(It.IsAny<IUpdateSolutionClientApplicationRequest>(), It.IsAny<CancellationToken>()), Times.Never());
        }

        private async Task<RequiredResult> UpdateBrowsersSupported(HashSet<string> browsersSupported, string mobileResponsive = null)
        {
            return await Context.UpdateSolutionBrowsersSupportedHandler.Handle(new UpdateSolutionBrowsersSupportedCommand("Sln1", new UpdateSolutionBrowsersSupportedViewModel()
            {
                BrowsersSupported = browsersSupported,
                MobileResponsive = mobileResponsive
            }), new CancellationToken())
                .ConfigureAwait(false);
        }
    }
}
