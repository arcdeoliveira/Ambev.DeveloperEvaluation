using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation
{
    public class SaleValidatorTest
    {
        private readonly SaleValidator _validationRules;
        public SaleValidatorTest()
        {
            _validationRules = new SaleValidator();
        }

        [Fact(DisplayName = "Valid sale should pass all validation rules")]
        public void Given_ValidUser_When_Validated_Then_ShouldNotHaveErrors()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            // Act
            var result = _validationRules.TestValidate(sale);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact(DisplayName = "Product sale cannot be empty, so must be invalid")]
        public void Given_UsernameLongerThanMaximum_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            sale.AlterItens([]);

            // Act
            var result = _validationRules.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ProductSales);
        }
    }
}
