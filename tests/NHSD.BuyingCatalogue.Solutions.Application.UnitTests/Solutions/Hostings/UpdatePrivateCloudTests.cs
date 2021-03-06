using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NHSD.BuyingCatalogue.Infrastructure.Exceptions;
using NHSD.BuyingCatalogue.Solutions.Application.Commands.Hostings.PrivateCloud;
using NHSD.BuyingCatalogue.Solutions.Application.Commands.Validation;
using NHSD.BuyingCatalogue.Solutions.Application.Domain;
using NHSD.BuyingCatalogue.Solutions.Contracts.Commands.Hostings;
using NHSD.BuyingCatalogue.Solutions.Contracts.Persistence;
using NUnit.Framework;

namespace NHSD.BuyingCatalogue.Solutions.Application.UnitTests.Solutions.Hostings
{
    [TestFixture]
    internal sealed class UpdatePrivateCloudTests : HostingTestsBase
    {
        private const string SolutionId = "Sln1";
        private const string SummaryToken = "PrivateCloud.Summary";
        private const string LinkToken = "PrivateCloud.Link";
        private const string HostingModelToken = "PrivateCloud.HostingModel";
        private const string RequiresHscnToken = "PrivateCloud.RequiresHSCN";

        private Mock<IUpdatePrivateCloudData> _dataMock;
        private string _updatedSummary;
        private string _updatedLink;
        private string _updatedHostingModel;
        private string _updatedRequiresHscn;

        private Hosting _initialHosting;
        private string _initialHostingJson;

        [SetUp]
        public void Setup()
        {
            _initialHosting = new Hosting

            {
                PrivateCloud = new PrivateCloud
                {
                    Summary = "A summary",
                    Link = "A link",
                    HostingModel = "A _initialHosting model",
                    RequiresHSCN = "A string"
                }
            };

            _initialHostingJson = JsonConvert.SerializeObject(_initialHosting);

            _updatedSummary = "A summary";
            _updatedLink = "A link";
            _updatedHostingModel = "A _initialHosting model";
            _updatedRequiresHscn = "A requires string";

        _dataMock = new Mock<IUpdatePrivateCloudData>();
            _dataMock.Setup(x => x.Summary).Returns(() => _updatedSummary);
            _dataMock.Setup(x => x.Link).Returns(() => _updatedLink);
            _dataMock.Setup(x => x.HostingModel).Returns(() => _updatedHostingModel);
            _dataMock.Setup(x => x.RequiresHSCN).Returns(() => _updatedRequiresHscn);
        }

        [Test]
        public async Task ValidDataShouldUpdatePrivateCloud()
        {
            SetUpMockSolutionRepositoryGetByIdAsync(_initialHostingJson);
            var validationResult = await Update().ConfigureAwait(false);

            Context.MockSolutionRepository.Verify(x => x.ByIdAsync(SolutionId, It.IsAny<CancellationToken>()), Times.Once);
            Context.MockSolutionDetailRepository.Verify(x => x.UpdateHostingAsync(It.Is<IUpdateSolutionHostingRequest>(
                c =>
                    c.SolutionId == SolutionId &&
                        JToken.Parse(c.Hosting).SelectToken(SummaryToken).Value<string>() == _updatedSummary &&
                        JToken.Parse(c.Hosting).SelectToken(LinkToken).Value<string>() == _updatedLink &&
                        JToken.Parse(c.Hosting).SelectToken(HostingModelToken).Value<string>() == _updatedHostingModel &&
                        JToken.Parse(c.Hosting).SelectToken(RequiresHscnToken).Value<string>() == _updatedRequiresHscn
                ), It.IsAny<CancellationToken>()), Times.Once);
            validationResult.IsValid.Should().BeTrue();
        }

        [Test]
        public async Task NullDataShouldUpdatePrivateCloud()
        {
            SetUpMockSolutionRepositoryGetByIdAsync(_initialHostingJson);

            _updatedSummary = null;
            _updatedLink = null;
            _updatedHostingModel = null;
            _updatedRequiresHscn = null;

            var validationResult = await Update().ConfigureAwait(false);

            Context.MockSolutionRepository.Verify(x => x.ByIdAsync(SolutionId, It.IsAny<CancellationToken>()), Times.Once);
            Context.MockSolutionDetailRepository.Verify(x => x.UpdateHostingAsync(It.Is<IUpdateSolutionHostingRequest>(
                c =>
                    c.SolutionId == SolutionId &&
                    JToken.Parse(c.Hosting).SelectToken("Hosting") == null
            ), It.IsAny<CancellationToken>()), Times.Once);
            validationResult.IsValid.Should().BeTrue();
        }

        [Test]
        public async Task ShouldUpdatePrivateCloudAndNothingElse()
        {
            SetUpMockSolutionRepositoryGetByIdAsync(_initialHostingJson);

            var calledBack = false;

            Context.MockSolutionDetailRepository
                .Setup(r => r.UpdateHostingAsync(It.IsAny<IUpdateSolutionHostingRequest>(),
                    It.IsAny<CancellationToken>()))
                .Callback((IUpdateSolutionHostingRequest updateHostingRequest,
                    CancellationToken cancellationToken) =>
                {
                    calledBack = true;
                    var json = JToken.Parse(updateHostingRequest.Hosting);
                    var newHosting = JsonConvert.DeserializeObject<Hosting>(json.ToString());
                    newHosting.Should().BeEquivalentTo(_initialHosting, c =>
                        c.Excluding(m => m.PrivateCloud));

                    newHosting.PrivateCloud.Summary.Should().Be(_updatedSummary);
                    newHosting.PrivateCloud.Link.Should().Be(_updatedLink);
                    newHosting.PrivateCloud.HostingModel.Should().Be(_updatedHostingModel);
                    newHosting.PrivateCloud.RequiresHSCN.Should().BeEquivalentTo(_updatedRequiresHscn);
                });
            var validationResult = await Update().ConfigureAwait(false);
            validationResult.IsValid.Should().BeTrue();

            Context.MockSolutionRepository.Verify(r => r.ByIdAsync(SolutionId, It.IsAny<CancellationToken>()), Times.Once());

            calledBack.Should().BeTrue();
        }

        [TestCase(500, 1000, 1001, "hosting-model")]
        [TestCase(500, 1001, 1000, "link")]
        [TestCase(501, 1000, 1000, "summary")]
        [TestCase(501, 1001, 1001, "summary", "link", "hosting-model")]
        public async Task MissingDataShouldReturnRequiredValidationResult(int summary, int link, int hosting, params string[] expected)
        {
            _updatedSummary = new string('a', summary);
            _updatedLink = new string('a', link);
            _updatedHostingModel = new string('a', hosting);

            SetUpMockSolutionRepositoryGetByIdAsync();
            var validationResult = await Update().ConfigureAwait(false);

            Context.MockSolutionRepository.Verify(x => x.ByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()),
                Times.Never);
            Context.MockSolutionDetailRepository.Verify(
                x => x.UpdateHostingAsync(It.IsAny<IUpdateSolutionHostingRequest>(),
                    It.IsAny<CancellationToken>()), Times.Never);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Should().BeOfType<MaxLengthResult>();
            var maxLengthResult = validationResult as MaxLengthResult;
            maxLengthResult.MaxLength.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void InvalidSolutionIdShouldThrowNotFound()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await Update().ConfigureAwait(false));
            Context.MockSolutionRepository.Verify(x => x.ByIdAsync(SolutionId, It.IsAny<CancellationToken>()),
                Times.Once);
            Context.MockSolutionDetailRepository.Verify(
                x => x.UpdateHostingAsync(It.IsAny<IUpdateSolutionHostingRequest>(),
                    It.IsAny<CancellationToken>()), Times.Never);
        }

        private async Task<ISimpleResult> Update()
        {
            return await Context.UpdatePrivateCloudHandler.Handle(
                new UpdatePrivateCloudCommand(SolutionId, _dataMock.Object),
                new CancellationToken()).ConfigureAwait(false);
        }
    }
}
