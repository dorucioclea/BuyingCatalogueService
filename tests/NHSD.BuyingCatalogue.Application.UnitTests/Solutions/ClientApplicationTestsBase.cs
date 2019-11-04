using System;
using System.Threading;
using Moq;
using NHSD.BuyingCatalogue.Contracts.Persistence;
using NUnit.Framework;

namespace NHSD.BuyingCatalogue.Application.UnitTests.Tools
{
    internal class ClientApplicationTestsBase
    {
        protected TestContext Context;

        [SetUp]
        public void SetUpFixture()
        {
            Context = new TestContext();
        }

        protected void SetUpMockSolutionRepositoryGetByIdAsync(string clientApplicationJson)
        {
            var existingSolution = new Mock<ISolutionResult>();

            existingSolution.Setup(s => s.Id).Returns("Sln1");

            existingSolution.Setup(s => s.ClientApplication).Returns(clientApplicationJson);

            Context.MockSolutionRepository.Setup(r => r.ByIdAsync("Sln1", It.IsAny<CancellationToken>())).ReturnsAsync(existingSolution.Object);
        }

        protected bool SetUpCallback(Action<string> assertFunc)
        {
            Context.MockMarketingDetailRepository
                .Setup(r => r.UpdateClientApplicationAsync(It.IsAny<IUpdateSolutionClientApplicationRequest>(), It.IsAny<CancellationToken>()))
                .Callback((IUpdateSolutionClientApplicationRequest updateSolutionClientApplicationRequest, CancellationToken cancellationToken) =>
                {
                    assertFunc(updateSolutionClientApplicationRequest.ClientApplication);
                });
            return true;
        }
    }
}
