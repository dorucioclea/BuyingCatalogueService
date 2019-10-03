using NHSD.BuyingCatalogue.Domain.Entities.Solutions;
using NUnit.Framework;
using Shouldly;

namespace NHSD.BuyingCatalogue.Domain.Tests.Solutions
{
    [TestFixture]
	public class SolutionTests
	{
        [Test]
        public void GivenSolution_CheckSupplierStatus_ShouldBeEqualToDraft()
        {
            //Arrange
            var expected = SupplierStatus.Draft;

            //Act
            var solution = new Solution();

            //Assert
            solution.SupplierStatus.ShouldBe(expected);
        }
    }
}
