using System;
using NHSD.BuyingCatalogue.Solutions.Application.Commands.SubmitForReview;
using NUnit.Framework;

namespace NHSD.BuyingCatalogue.Application.UnitTests.Solutions
{
    [TestFixture]
    internal sealed class ValidationErrorTests
    {
        [Test]
        public void ShouldThrowOnNullId()
        {
            Assert.Throws<ArgumentException>(() => new ValidationError(null));
        }
    }
}