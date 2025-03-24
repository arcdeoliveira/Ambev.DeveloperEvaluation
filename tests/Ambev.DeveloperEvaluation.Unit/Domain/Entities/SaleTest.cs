using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleTest
    {
        [Theory(DisplayName = "Sale status should change when a new status is allocate")]
        [InlineData(SaleStatus.Modified)]
        [InlineData(SaleStatus.Cancelled)]
        public void  Give_A_New_Sale_Status_Then_Sale_Should_Be_With_The_New_status(SaleStatus saleStatus)
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            var oldStatus = sale.Status;

            // Act
            sale.AlterStatus(saleStatus);

            // Assert
            sale.CreatedAt.Should().NotBe(DateTime.MinValue);
            sale.UpdatedAt.Should().BeNull();

            sale.Status.Should().NotBe(SaleStatus.None);
            sale.Status.Should().NotBe(oldStatus);
            sale.Status.Should().Be(saleStatus);
        }

        [Fact(DisplayName = "Sale userId should be updated when give a new userId")]
        public void Given_A_New_Customer_As_User_Id_Then_Sale_Update_User_Id()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var sale = SaleTestData.GenerateValidSale();
            var previousUserId = sale.UserId;

            // Act           
            sale.AlterCostumer(userId);

            // Assert
            sale.CreatedAt.Should().NotBe(DateTime.MinValue);
            sale.UpdatedAt.Should().BeNull();

            sale.UserId.Should().NotBe(Guid.Empty);
            sale.Should().NotBeSameAs(previousUserId);
            sale.UserId.Should().Be(userId);
        }


        [Theory(DisplayName = "Sale affiliateID should change when give a new affiliateId")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Give_A_New_Affiliate_Id_Then_Sale_Should_Be_With_The_New_Affiliate_Id(short affiliateId)
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            var previousAffiliateId = sale.AfiliateId;

            // Act
            sale.AlterAffiliate(affiliateId);

            // Assert
            sale.CreatedAt.Should().NotBe(DateTime.MinValue);
            sale.UpdatedAt.Should().BeNull();

            sale.AfiliateId.Should().NotBe(previousAffiliateId);
            sale.AfiliateId.Should().Be(affiliateId);
        }

        [Theory(DisplayName = "Sale total and productSale count should change when give a new collection of productSale")]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(30)]
        public void Give_A_New_Set_Of_ProductSale_Then_Sale_Should_Be_With_Diferent_Total_Value(int newQuantity)
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            var previousProductSalesCount = sale.ProductSales.Count();
            var previousTotal = sale.Total;

            var prodcutSale = ProductSaleData.GenerateValidProductSales(newQuantity);
            

            // Act
            sale.AlterItens(prodcutSale);
            sale.SetTotal();

            // Assert
            sale.CreatedAt.Should().NotBe(DateTime.MinValue);
            sale.UpdatedAt.Should().BeNull();

            sale.Total.Should().NotBe(previousTotal);

            sale.ProductSales.Should().NotBeEmpty();
            sale.ProductSales.Should().NotHaveCount(previousProductSalesCount);
            sale.ProductSales.Should().HaveCount(newQuantity);
        }
    }
}
