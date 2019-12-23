using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NHSD.BuyingCatalogue.Solutions.API.Controllers;
using NHSD.BuyingCatalogue.Solutions.API.ViewModels;
using NHSD.BuyingCatalogue.Solutions.Application.Commands.UpdateSolutionPlugins;
using NHSD.BuyingCatalogue.Solutions.Application.Commands.Validation;
using NHSD.BuyingCatalogue.Solutions.Contracts;
using NHSD.BuyingCatalogue.Solutions.Contracts.Queries;
using NUnit.Framework;

namespace NHSD.BuyingCatalogue.Solutions.API.UnitTests
{
    [TestFixture]
    public sealed class PluginsControllerTests
    {
        private Mock<IMediator> _mockMediator;

        private PlugInsController _plugInsController;

        private const string SolutionId = "Sln1";

        [SetUp]
        public void Setup()
        {
            _mockMediator = new Mock<IMediator>();
            _plugInsController = new PlugInsController(_mockMediator.Object);
        }

        [Test]
        public async Task ShouldGetPlugins()
        {
            _mockMediator.Setup(m =>
                    m.Send(It.Is<GetClientApplicationBySolutionIdQuery>(q => q.Id == SolutionId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Mock.Of<IClientApplication>(s =>
                    s.Plugins == Mock.Of<IPlugins>(c =>
                        c.Required == true && c.AdditionalInformation == "Additional Information")));

            var result = (await _plugInsController.GetPlugInsAsync(SolutionId).ConfigureAwait(false)) as ObjectResult;
            result?.StatusCode.Should().Be((int)HttpStatusCode.OK);

            var plugin = (result?.Value as GetPlugInsResult);
            plugin?.PlugIns.Should().Be("Yes");
            plugin?.AdditionalInformation.Should().Be("Additional Information");

            _mockMediator.Verify(
                m => m.Send(It.Is<GetClientApplicationBySolutionIdQuery>(q => q.Id == SolutionId),
                    It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestCase(null, null, null)]
        [TestCase(true, "Yes", null)]
        [TestCase(true, "Yes", "info")]
        [TestCase(false, "No", null)]
        [TestCase(false, "No", "add info")]
        public async Task ShouldGetPluginRequired(bool? pluginRequired, string expectedPlugin, string additionalInfo)
        {
            _mockMediator.Setup(m => m
                    .Send(It.Is<GetClientApplicationBySolutionIdQuery>(q => q.Id == SolutionId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Mock.Of<IClientApplication>(s =>
                    s.Plugins == Mock.Of<IPlugins>(c =>
                        c.Required == pluginRequired && c.AdditionalInformation == additionalInfo)));

            var result = (await _plugInsController.GetPlugInsAsync(SolutionId).ConfigureAwait(false)) as ObjectResult;
            result?.StatusCode.Should().Be((int)HttpStatusCode.OK);

            var plugin = (result?.Value as GetPlugInsResult);
            plugin?.PlugIns.Should().Be(expectedPlugin);
            plugin?.AdditionalInformation.Should().Be(additionalInfo);

            _mockMediator.Verify(
                m => m.Send(It.Is<GetClientApplicationBySolutionIdQuery>(q => q.Id == SolutionId),
                    It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task ShouldGetPluginsIsNull()
        {
            var clientMock = new Mock<IClientApplication>();
            clientMock.Setup(c => c.Plugins).Returns<IPlugins>(null);

            _mockMediator.Setup(m =>
                    m.Send(It.Is<GetClientApplicationBySolutionIdQuery>(q => q.Id == SolutionId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(clientMock.Object);

            var result = (await _plugInsController.GetPlugInsAsync(SolutionId).ConfigureAwait(false)) as ObjectResult;
            result?.StatusCode.Should().Be((int)HttpStatusCode.OK);

            var plugin = (result?.Value as GetPlugInsResult);
            plugin?.AdditionalInformation.Should().BeNull();
            plugin?.PlugIns.Should().BeNull();
        }

        [Test]
        public async Task ShouldReturnNotFound()
        {
            var result = (await _plugInsController.GetPlugInsAsync(SolutionId).ConfigureAwait(false)) as NotFoundResult;

            result?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

            _mockMediator.Verify(
                m => m.Send(It.Is<GetClientApplicationBySolutionIdQuery>(q => q.Id == SolutionId),
                    It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task ShouldUpdateValidationValid()
        {
            var pluginsViewModel = new UpdateSolutionPluginsViewModel();

            var validationModel = new RequiredMaxLengthResult();

            _mockMediator
                .Setup(m => m.Send(
                    It.Is<UpdateSolutionPluginsCommand>(q =>
                        q.SolutionId == SolutionId && q.UpdateSolutionPluginsViewModel == pluginsViewModel),
                    It.IsAny<CancellationToken>())).ReturnsAsync(validationModel);

            var result = (await _plugInsController.UpdatePlugInsAsync(SolutionId, pluginsViewModel).ConfigureAwait(false)) as NoContentResult;

            result?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

            _mockMediator.Verify(
                m => m.Send(
                    It.Is<UpdateSolutionPluginsCommand>(q =>
                        q.SolutionId == SolutionId && q.UpdateSolutionPluginsViewModel ==
                        pluginsViewModel), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task ShouldUpdateValidationInvalid()
        {
            var pluginsViewModel = new UpdateSolutionPluginsViewModel();

            var validationModel = new RequiredMaxLengthResult()
            {
                Required = { "plugins-required" },
                MaxLength = { "plugins-detail" }
            };

            _mockMediator.Setup(m =>
                m.Send(
                    It.Is<UpdateSolutionPluginsCommand>(q =>
                        q.SolutionId == SolutionId && q.UpdateSolutionPluginsViewModel == pluginsViewModel),
                    It.IsAny<CancellationToken>())).ReturnsAsync(validationModel);

            var result = (await _plugInsController.UpdatePlugInsAsync(SolutionId, pluginsViewModel).ConfigureAwait(false)) as BadRequestObjectResult;

            result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result.Value as UpdateFormRequiredMaxLengthResult).Required.Should().BeEquivalentTo(new[] { "plugins-required" });
            (result.Value as UpdateFormRequiredMaxLengthResult).MaxLength.Should().BeEquivalentTo(new[] { "plugins-detail" });

            _mockMediator.Verify(
                m => m.Send(
                    It.Is<UpdateSolutionPluginsCommand>(q =>
                        q.SolutionId == SolutionId && q.UpdateSolutionPluginsViewModel ==
                        pluginsViewModel), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
