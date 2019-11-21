using System;
using NHSD.BuyingCatalogue.Application.Solutions.Commands.SubmitForReview;
using NUnit.Framework;

namespace NHSD.BuyingCatalogue.Application.UnitTests.Solutions
{
    [TestFixture]
    internal sealed class ValidationResultTests
    {
        private ValidationResult _validationResult;

        [SetUp]
        public void Setup()
        {
            _validationResult = new ValidationResult();
        }

        [Test]
        public void ShouldThrowAddNullValidationError()
        {
            Assert.Throws<ArgumentNullException>(() => _validationResult.Add((ValidationError)null));
        }

        [Test]
        public void ShouldThrowAddNullValidationResult()
        {
            Assert.Throws<ArgumentNullException>(() => _validationResult.Add((ValidationResult)null));
        }
    }
}
