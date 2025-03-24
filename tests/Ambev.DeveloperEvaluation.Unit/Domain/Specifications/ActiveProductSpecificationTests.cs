using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.Unit.Domain.Specifications.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications
{
    public class ActiveProductSpecificationTests
    {
        [Theory]
        [InlineData(ProductStatus.Active, true)]
        [InlineData(ProductStatus.Inactive, false)]
        [InlineData(ProductStatus.OutOfStock, false)]
        [InlineData(ProductStatus.Discontinued, false)]
        public void IsSatisfiedBy_ShouldValidateProductStatus(ProductStatus status, bool expectedResult)
        {
            // Arrange
            var product = ActiveProductSpecificationTestData.CreateProductWithStatus(status);
            var specification = new ActiveProductSpecification();

            // Act
            var result = specification.IsSatisfiedBy(product);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
