using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class ProductSaleTest
    {
        [Fact(DisplayName = "Product sale on cancelling must be cancel true and date of canceled not null")]
        public void Given_Cancel_Product_Sale_Then_Must_Be_Canceled_And_Date_Informed()
        {
            // Arrange
            var productSaleList = ProductSaleData.GenerateValidProductSales(5);

            // Act
            foreach (var productSale in productSaleList)
            {
                productSale.Cancel();
            }

            // Assert
            productSaleList.All(a => a.Canceled).Should().BeTrue();
            productSaleList.All(a => a.DateCanceled != null).Should().BeTrue();
        }

        [Fact(DisplayName = "Product sale productId should change when a nem productId is given.")]
        public void Given_a_newProductId_To_Product_Sale_Then_ProductId_Must_Be_Updated()
        {
            // Arrange
            var productSaleList = ProductSaleData.GenerateValidProductSales(1);
            var productSale = productSaleList.FirstOrDefault() ?? new ProductSale();

            var previousProductId = productSale.ProductId;
            var newProductId = Guid.NewGuid().ToString();

            // Act
            productSale.AlterProduct(newProductId);

            // Assert
            productSale.Canceled.Should().BeFalse();
            productSale.DateCanceled.Should().BeNull();

            productSale.ProductId.Should().Be(newProductId);
            productSale.ProductId.Should().NotBe(previousProductId);
        }
    }
}
