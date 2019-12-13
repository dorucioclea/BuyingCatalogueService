using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NHSD.BuyingCatalogue.Solutions.API.Controllers;
using NHSD.BuyingCatalogue.Solutions.API.ViewModels;
using NHSD.BuyingCatalogue.Solutions.Application.Commands.UpdateSolutionBrowserHardwareRequirements;
using NHSD.BuyingCatalogue.Solutions.Application.Queries.GetSolutionById;
using NHSD.BuyingCatalogue.Solutions.Contracts;
using NHSD.BuyingCatalogue.Solutions.Contracts.Queries;
using NUnit.Framework;

namespace NHSD.BuyingCatalogue.Solutions.API.UnitTests
{
    [TestFixture]
    public sealed class BrowserHardwareRequirementsControllerTests
    {
        private Mock<IMediator> _mockMediator;

        private BrowserHardwareRequirementsController _hardwareRequirementsController;

        private const string SolutionId = "Sln1";

        [SetUp]
        public void Setup()
        {
            _mockMediator = new Mock<IMediator>();
            _hardwareRequirementsController = new BrowserHardwareRequirementsController(_mockMediator.Object);
        }

        [TestCase(null)]
        [TestCase("Hardware Info")]
        public async Task ShouldGetBrowserHardwareRequirement(string requirement)
        {
            _mockMediator.Setup(m =>
                    m.Send(It.Is<GetClientApplicationBySolutionIdQuery>(q => q.Id == SolutionId),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(Mock.Of<IClientApplication>(c =>
                    c.HardwareRequirements == requirement));

            var result =
                (await _hardwareRequirementsController.GetHardwareRequirementsAsync(SolutionId)
                    .ConfigureAwait(false)) as ObjectResult;

            result?.StatusCode.Should().Be((int)HttpStatusCode.OK);

            var browserHardwareRequirements = result?.Value as GetBrowserHardwareRequirementsResult;

            browserHardwareRequirements?.HardwareRequirements.Should().Be(requirement);
            _mockMediator.Verify(
                m => m.Send(It.Is<GetClientApplicationBySolutionIdQuery>(q => q.Id == SolutionId), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Test]
        public async Task ShouldReturnNotFound()
        {
            var result = (await _hardwareRequirementsController.GetHardwareRequirementsAsync(SolutionId).ConfigureAwait(false)) as NotFoundResult;

            result?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

            _mockMediator.Verify(
                m => m.Send(It.Is<GetClientApplicationBySolutionIdQuery>(q => q.Id == SolutionId),
                    It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task ShouldUpdateValidationValid()
        {
            var viewModel = new UpdateSolutionBrowserHardwareRequirementsViewModel();

            var validationResult = new UpdateSolutionBrowserHardwareRequirementsValidationResult();

            _mockMediator
                .Setup(m => m.Send(
                    It.Is<UpdateSolutionBrowserHardwareRequirementsCommand>(q =>
                        q.SolutionId == SolutionId && q.UpdateSolutionHardwareRequirementsViewModel == viewModel),
                    It.IsAny<CancellationToken>())).ReturnsAsync(validationResult);

            var result = await _hardwareRequirementsController.UpdateHardwareRequirementsAsync(SolutionId, viewModel)
                .ConfigureAwait(false) as NoContentResult;

            result?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
            _mockMediator.Verify(
                m => m.Send(
                    It.Is<UpdateSolutionBrowserHardwareRequirementsCommand>(q =>
                        q.SolutionId == SolutionId && q.UpdateSolutionHardwareRequirementsViewModel ==
                        viewModel), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task ShouldUpdateValidationInvalid()
        {
            var viewModel = new UpdateSolutionBrowserHardwareRequirementsViewModel();

            var validationResult = new UpdateSolutionBrowserHardwareRequirementsValidationResult()
            {
                MaxLength = { "hardware-requirements-description" }
            };

                _mockMediator.Setup(m => m.Send(
                It.Is<UpdateSolutionBrowserHardwareRequirementsCommand>(q =>
                    q.SolutionId == SolutionId && q.UpdateSolutionHardwareRequirementsViewModel == viewModel),
                It.IsAny<CancellationToken>())).ReturnsAsync(validationResult);

            var result = await _hardwareRequirementsController.UpdateHardwareRequirementsAsync(SolutionId, viewModel)
                .ConfigureAwait(false) as BadRequestObjectResult;

            result?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result?.Value as UpdateSolutionBrowserHardwareRequirementResult)?.MaxLength.Should().BeEquivalentTo(new [] { "hardware-requirements-description" });

            _mockMediator.Verify(
                m => m.Send(
                    It.Is<UpdateSolutionBrowserHardwareRequirementsCommand>(q =>
                        q.SolutionId == SolutionId && q.UpdateSolutionHardwareRequirementsViewModel ==
                        viewModel), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
